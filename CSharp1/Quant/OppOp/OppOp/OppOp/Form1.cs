using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OppOp
{
    public partial class Form1 : Form
    {
        List<Calculator> Calculators = new List<Calculator>();
        List<double> Tickers = new List<double>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader TickersReader = new StreamReader(@"U:\Vladimir Timerman\OppClose\OppCloseTickers.csv");           

            int i = 0;
            TickersReader.ReadLine();
            while (!TickersReader.EndOfStream)
            {
                string[] sep = { ";" };

                string[] values = TickersReader.ReadLine().Split(sep, StringSplitOptions.RemoveEmptyEntries);

                Tickers.Add(int.Parse(values[1]));
            }

            OppOpData.Instance.RunDate = new DateTime(2008, 01, 01);
            OppOpData.Instance.InitializeObjects();
            OppOpData.Instance.LoadTickers(Tickers.ToArray());

            foreach (int ticker in Tickers)
            {
                if (ticker == 347)
                {
                    Calculators.Add(new Calculator(ticker, OppOpData.Instance.RunDate, DateTime.Today));
                }
            }

            PrintStatistics();
        }

        private void PrintStatistics()
        {
            List<DateTime> RunDates = Utils.getBovOpenDates(new DateTime(2008, 01, 01));
            Dictionary<DateTime, List<string>> StatisticsProbUp = new Dictionary<DateTime, List<string>>();
            Dictionary<DateTime, List<string>> StatisticsProbDown = new Dictionary<DateTime, List<string>>();

            string header = ";";

            foreach (Calculator Calc in Calculators)
            {
                header += Calc.ToString() + ";";                
            }

            foreach (DateTime Date in RunDates)
            {
                StatisticsProbDown.Add(Date, new List<string>());
                StatisticsProbUp.Add(Date, new List<string>());
            }

            foreach (KeyValuePair<DateTime, List<string>> Node in StatisticsProbUp)
            {
                foreach (Calculator Calc in Calculators)
                {
                    if (Calc.TickerDataList.ContainsKey(Node.Key))
                    {
                        if (Calc.TickerDataList[Node.Key].ProbUp > 0)
                        {
                            Node.Value.Add(Calc.TickerDataList[Node.Key].ProbUp.ToString().Replace(".", ","));
                        }
                        else
                        {
                            Node.Value.Add(";");
                        }
                    }
                    else
                    {
                        Node.Value.Add(";");
                    }
                }
            }

            foreach (KeyValuePair<DateTime, List<string>> Node in StatisticsProbDown)
            {
                foreach (Calculator Calc in Calculators)
                {
                    if (Calc.TickerDataList.ContainsKey(Node.Key))
                    {
                        if (Calc.TickerDataList[Node.Key].ProbDown > 0)
                        {
                            Node.Value.Add(Calc.TickerDataList[Node.Key].ProbDown.ToString());
                        }
                        else
                        {
                            Node.Value.Add(";");
                        }
                    }
                    else
                    {
                        Node.Value.Add(";");
                    }
                }
            }

            StreamWriter ProbUpWriter = new StreamWriter(@"U:\Vladimir Timerman\OppOp\ProbUp.csv");
            StreamWriter ProbDownWriter = new StreamWriter(@"U:\Vladimir Timerman\OppOp\ProbDown.csv");

            ProbUpWriter.WriteLine(header);
            ProbDownWriter.WriteLine(header);

            foreach (KeyValuePair<DateTime, List<string>> Node in StatisticsProbUp)
            {
                if (WriteDate(Node.Value))
                {
                    string Line = Node.Key.ToString("dd/MM/yyyy");
                    foreach (string prob in Node.Value)
                    {
                        Line += prob.Replace(".", ",") + ";";
                    }
                    ProbUpWriter.WriteLine(Line);
                }
            }

            foreach (KeyValuePair<DateTime, List<string>> Node in StatisticsProbDown)
            {
                if (WriteDate(Node.Value))
                {
                    string Line = Node.Key.ToString("dd/MM/yyyy");
                    foreach (string prob in Node.Value)
                    {
                        Line += prob.Replace(".", ",") + ";";
                    }
                    ProbDownWriter.WriteLine(Line);
                }
            }
        }    
    
        private static bool WriteDate(List<string> ProbList)
        {
            for (int i = 0; i < ProbList.Count; i++)
            {
                if (double.Parse(ProbList[i]) != 0 && !double.IsNaN(double.Parse(ProbList[i]))) return true;
            }
            return false;
        }
    }
}
