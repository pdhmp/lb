using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ProcessFile(@"C:\Users\vladimir.timerman\Desktop\Oraculo\Indice_1hora.txt");

        }
        private void ProcessFile(string curFileName)
        {

            FileStream fs = new FileStream(curFileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            string tempLine = "";
            int tamanho=0;


       
            tempLine = sr.ReadLine();

            while ((tempLine = sr.ReadLine()) != null)
            {
                tamanho++;
            }
            sr.Close();
            fs.Close();
            

            DateTime[] dia=new DateTime[tamanho];
            TimeSpan[] hora=new TimeSpan[tamanho];
            double[] ultimo = new double[tamanho];
            double[] variação = new double[tamanho];
            double[] media=new double[tamanho];

            int cont = 0;

            fs = new FileStream(curFileName, FileMode.Open, FileAccess.Read);
            sr = new StreamReader(fs);
            StreamWriter swMedia = new StreamWriter(@"C:\Users\vladimir.timerman\Desktop\Oraculo\Media.txt");
            StreamWriter swVariação = new StreamWriter(@"C:\Users\vladimir.timerman\Desktop\Oraculo\Variação.txt");
            tempLine = sr.ReadLine();
            while ((tempLine = sr.ReadLine()) != null)
            {
                string[] curValues = tempLine.Split('\t');
                dia[cont] = DateTime.Parse(curValues[0]);
                hora[cont] = TimeSpan.Parse(curValues[1]);
                ultimo[cont] =double.Parse(curValues[6]);
                cont++;
                
            }

            
            int lag = 24;
            int lagvar = 3;
         
            double soma = 0;
            double medianm1 = 0;

            variação[0]=double.NaN;
            media[0] = double.NaN;
            for (cont = 1; cont < tamanho; cont++)
            {
                if (cont >= lag)
                {
                    soma = 0;
                    for (int i = 0; i < lag; i++)
                    {
                        soma = soma + ultimo[cont - i - 1];
                    }
                    media[cont] = (soma / lag);
                }
                else
                {
                    media[cont] = double.NaN;
                }
                if (cont >= lag + 1)
                {
                    variação[cont]=(((soma / lag) / medianm1)-1);
                }
                else
                {
                    variação[cont] = double.NaN;
                }
                medianm1 = soma / lag;
            }
            for (cont = 1; cont < tamanho; cont++)
			{
			 soma=0;
                for (int i = 0; i < lagvar; i++)
			{
			 
			}


			}
        }

    }
}
