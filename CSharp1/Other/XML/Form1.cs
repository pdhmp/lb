using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    XmlDocument documento = new XmlDocument();

                    documento.Load(openFileDialog1.FileName);

                    XmlNodeList arrNodes = documento.SelectNodes("/arquivoposicao_4_00/fundo/acoes");

                    // pegar um nodo filho no xml .. exemplo com xpath
                    foreach (XmlNode node in arrNodes)
                    {
                        XmlNode objIndexador = node.SelectSingleNode("classeoperacao");
                        if (objIndexador.InnerText == "D")
                        {
                         XmlNode objPerc = node.SelectSingleNode("cnpjinter");
                         // objPerc.InnerText = "00000000000000";

                          XmlNode objdt = node.SelectSingleNode("dtvencalug");
                          //objdt.InnerText = objdt.InnerText+1;
                         }
                    }
                    arrNodes = null;
//************************************************************
                    XmlNodeList arrNodes2 = documento.SelectNodes("/arquivoposicao_4_00/fundo/futuros");

                    // pegar um nodo filho no xml .. exemplo com xpath
                    foreach (XmlNode node in arrNodes2)
                    {
                        XmlNode objcorretora = node.SelectSingleNode("cnpjcorretora");
                        objcorretora.InnerText = "00000000000000";
                    }

//*********************************************************                    
//************************************************************
                    XmlNodeList arrNodes3 = documento.SelectNodes("/arquivoposicao_4_00/fundo/swap");

                    // pegar um nodo filho no xml .. exemplo com xpath
                    foreach (XmlNode node in arrNodes3)
                    {
                        XmlNode objcorretora = node.SelectSingleNode("cnpjcontraparte");
                        objcorretora.InnerText = "00000000000000";
                    }
//*********************************************************                    
//*********************************************************
                   XmlNodeList arrNodes5 = documento.SelectNodes("/arquivoposicao_4_00/fundo/futuros");

                    // pegar um nodo filho no xml .. exemplo com xpath
                    foreach (XmlNode node in arrNodes2)
                    {
                        XmlNode objcorretora = node.SelectSingleNode("cnpjcorretora");
                       // objcorretora.InnerText = "00000000000000";
                    }

//*********************************************************                    
//************************************************************

                    XmlNodeList arrNodes6 = documento.SelectNodes("/arquivoposicao_4_00/fundo/header");

                    // pegar um nodo filho no xml .. exemplo com xpath
                    foreach (XmlNode node in arrNodes6)
                    {
                        XmlNode objnivel = node.SelectSingleNode("nivelrsc");
                     //   objnivel.InnerText = "BB";
                    }
//*********************************************************                    
                    //string out_doc = documento.OuterXml;
                    documento.Save(openFileDialog1.FileName);
                    MessageBox.Show("Preparação de Documento feita com Sucesso!");
                    Application.Exit();
                }
            }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        }
}
