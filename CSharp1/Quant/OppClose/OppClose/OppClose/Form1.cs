using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OppClose
{
    public partial class Form1 : Form
    {
        List<Ticker> Tickers;
        
        public Form1()
        {
            InitializeComponent();
            Tickers = new List<Ticker>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader TickersReader = new StreamReader(@"U:\Vladimir Timerman\OppClose\OppCloseTickers.csv");

            int i = 0;
            TickersReader.ReadLine();
            while (!TickersReader.EndOfStream)
            {
                string[] sep = {";"};                
                
                string[] values = TickersReader.ReadLine().Split(sep,StringSplitOptions.RemoveEmptyEntries);

                //if (values[0].Contains("BOVA11"))
                //{
                    Tickers.Add(new Ticker(int.Parse(values[1]), DateTime.Parse(values[2]), DateTime.Parse(values[3])));
                //}
            }

            PrintStatistics();
        }

        private void PrintStatistics()
        {
            List<string> ProbUpRows = new List<string>();
            List<string> ProbDownRows = new List<string>();
            List<string> VarRows = new List<string>();

            string ProbUp = "";
            string ProbDown = "";
            string Var = "";

            foreach (Ticker papel in Tickers)
            {
                ProbUp += papel.ToString() + ";";
                ProbDown += papel.ToString() + ";";
                Var += papel.ToString() + ";";
            }

            StreamWriter SwProbUp = new StreamWriter(@"U:\Vladimir Timerman\OppClose\OppCloseProbUpTestSemEst.csv");
            StreamWriter SwProbDown = new StreamWriter(@"U:\Vladimir Timerman\OppClose\OppCloseProbDownTestSemEst.csv");
            StreamWriter SwVar = new StreamWriter(@"U:\Vladimir Timerman\OppClose\OppCloseVarTestSemEst.csv");

            /*StreamWriter SwProbUp = new StreamWriter(@"U:\Vladimir Timerman\OppClose\OppCloseProbUpTest.csv");
            StreamWriter SwProbDown = new StreamWriter(@"U:\Vladimir Timerman\OppClose\OppCloseProbDownTest.csv");
            StreamWriter SwVar = new StreamWriter(@"U:\Vladimir Timerman\OppClose\OppCloseVarTest.csv");*/

            SwProbUp.WriteLine(@";" + ProbUp);
            SwProbDown.WriteLine(@";" + ProbDown);
            SwVar.WriteLine(@";" + Var);

            ProbUp = ProbDown = Var = "";

            foreach (DateTime date in Tickers[0].Statistics.Keys)
            {
                string Row = date.ToString("dd/MM/yyyy");

                foreach (Ticker papel in Tickers)
                {
                    DayValues Values;
                    if (papel.Statistics.TryGetValue(date, out Values))
                    {
                        ProbUp += Values.ProbUp.ToString() + ";";
                        ProbDown += Values.ProbDown.ToString() + ";";
                        Var += Values.Var.ToString() + ";";
                    }
                    else
                    {
                        ProbUp += @"N\A;";
                        ProbDown += @"N\A;";
                        Var += @"N\A;";
                    }
                }

                SwProbUp.WriteLine(@Row + ";" + ProbUp);
                SwProbDown.WriteLine(@Row + ";" + ProbDown);
                SwVar.WriteLine(@Row + ";" + Var);

                ProbUp = ProbDown = Var = "";
            }

        }       

    }
}
