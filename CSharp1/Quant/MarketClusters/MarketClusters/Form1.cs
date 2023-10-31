using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MarketClusters
{
    public partial class Form1 : Form
    {
        ClusterCalc thisCluster = new ClusterCalc();
        int[,] DrawArray;
        int DrawFontSize = 1;
        int curOffset = 1;
        System.Drawing.Graphics graphicsObj;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void cmdRun_Click(object sender, EventArgs e)
        {
        }

        private void cmdCorrel_Click(object sender, EventArgs e)
        {
            cmdCorrel.Enabled = false;
            
            thisCluster.iniDate = new DateTime(2010, 07, 19);
            thisCluster.endDate = new DateTime(2020, 01, 01);

            if(File.Exists("C:\\temp\\correlOut.txt")) File.Delete("C:\\temp\\correlOut.txt");
            if(File.Exists("C:\\temp\\groupsOut.txt")) File.Delete("C:\\temp\\groupsOut.txt");
            if (File.Exists("C:\\temp\\draworderOut.txt")) File.Delete("C:\\temp\\groupsOut.txt"); 

            thisCluster.LoadAllSeries();

            for (int i = 0; i < thisCluster.serCount - 1; i++)
            {
                thisCluster.Clusterize();
            }

            thisCluster.ReorderGroups(thisCluster.serCount - 2);

            //thisCluster.ForceCorrelIncrease();

            graphicsObj = picDrawArea.CreateGraphics();
            InitializeDrawArray();
            DrawTree();


        }

        private void InitializeDrawArray()
        {
            DrawArray = new int[thisCluster.serCount * 2 - 1, 5];

            for (int i = 0; i < thisCluster.serCount; i++)
            {
                DrawArray[i, 0] = i;
                DrawArray[i, 1] = thisCluster.DrawOrder[i];
                DrawArray[i, 2] = 0;
                DrawArray[i, 3] = -1;
                DrawArray[i, 4] = -1;
            }

            for (int i = 0; i < thisCluster.serCount - 1; i++)
            {
                DrawArray[i + thisCluster.serCount, 0] = 0;
                DrawArray[i + thisCluster.serCount, 1] = i + 1;
                DrawArray[i + thisCluster.serCount, 2] = 1;
                DrawArray[i + thisCluster.serCount, 3] = -1;
                DrawArray[i + thisCluster.serCount, 4] = -1;
            }
        }

        private void RecalcDrawArray()
        {
            DrawFontSize = (int)(picDrawArea.Height / thisCluster.DrawOrderCounter) - 2;
            curOffset = (int)((DrawFontSize - 1) * 7 * 1.8F);

            for (int i = 0; i < (DrawArray.Length / 5); i++)
            {
                if(DrawArray[i, 2] == 0)
                {
                    if (DrawArray[i, 1] != 0)
                    {
                        DrawArray[i, 3] = curOffset;
                        DrawArray[i, 4] = (DrawFontSize + 2) * i + (int)((DrawFontSize + 2) / 2);
                    }
                }
                if (DrawArray[i, 2] == 1)
                {
                    if (DrawArray[i, 1] != 0)
                    {
                        DrawArray[i, 3] = GetGroupXValue(DrawArray[i, 1]);
                        DrawArray[i, 4] = GetGroupYValue(DrawArray[i, 1]);
                    }
                }
            }
        }

        private int GetGroupXValue(int GroupNumber)
        {
            for (int i = 0; i < thisCluster.SortedCounter; i++)
            {
                if (thisCluster.SortedGroups[i, 6] == GroupNumber)
                {
                    int PixelVal = convertCorrelToPixel(thisCluster.SortedGroups[i, 1]);
                    return PixelVal;
                }
            }
            return -1;
        }

        private int convertCorrelToPixel(double CorrelVal)
        {
            double PercentVal = 1 - CorrelVal;
            return (int)((picDrawArea.Width - curOffset) * PercentVal) + curOffset;
        
        }

        private int GetGroupYValue(int GroupNumber)
        {
            int groupPosition = -1;
            for (int i = 0; i < thisCluster.SortedCounter; i++)
            {
                if (thisCluster.SortedGroups[i, 6] == GroupNumber)
                {
                    groupPosition = i;
                }
            }

            int ser1 = (int)thisCluster.SortedGroups[groupPosition, 2];
            int ser2 = (int)thisCluster.SortedGroups[groupPosition, 4];
            int type1 = (int)thisCluster.SortedGroups[groupPosition, 3];
            int type2 = (int)thisCluster.SortedGroups[groupPosition, 5];

            int ser1Y = GetSerXYValue(ser1, type1)[1];
            int ser2Y = GetSerXYValue(ser2, type2)[1];
            return (int)(ser1Y + ser2Y) / 2;
        }

        private int[] GetSerXYValue(int SerID, int SerType)
        {
            int[] returnArray = new int[2];
            for (int i = 0; i < (DrawArray.Length / 5); i++)
            {
                if (DrawArray[i, 1] == SerID && DrawArray[i, 2] == SerType)
                {
                    returnArray[0] = DrawArray[i, 3];
                    returnArray[1] = DrawArray[i, 4];
                    return returnArray;
                }
            }

            returnArray[0] = -1;
            returnArray[1] = -1;
            return returnArray;
        }

        private void DrawTree()
        {
            RecalcDrawArray();

            Font tickerFont = new System.Drawing.Font("Helvetica", DrawFontSize - 1, FontStyle.Regular);
            Brush tickerBrush = new SolidBrush(System.Drawing.Color.Black);

            for (int i = 0; i < thisCluster.DrawOrderCounter; i++)
            {
                string curTicker = Utils.GetTickerSymbol(thisCluster.DrawOrder[i]);
                graphicsObj.DrawString(curTicker, tickerFont, tickerBrush, 10, (DrawFontSize + 2) * i);
            }

            Pen myPen = new Pen(System.Drawing.Color.LightGray, 1);
            graphicsObj.DrawLine(myPen, convertCorrelToPixel(0.0F), 0, convertCorrelToPixel(0.0F), picDrawArea.Height);
            graphicsObj.DrawLine(myPen, convertCorrelToPixel(0.25F), 0, convertCorrelToPixel(0.25F), picDrawArea.Height);
            graphicsObj.DrawLine(myPen, convertCorrelToPixel(0.50F), 0, convertCorrelToPixel(0.50F), picDrawArea.Height);
            graphicsObj.DrawLine(myPen, convertCorrelToPixel(0.75F), 0, convertCorrelToPixel(0.75F), picDrawArea.Height);
            graphicsObj.DrawLine(myPen, convertCorrelToPixel(1F), 0, convertCorrelToPixel(1F), picDrawArea.Height);

            for (int i = 0; i < (DrawArray.Length / 5); i++)
            {
                if (DrawArray[i, 2] == 1)
                {
                    DrawGroupLine(DrawArray[i, 1]);
                }
            }
        }


        private void DrawGroupLine(int GroupNumber)
        {
            Pen myPen = new Pen(System.Drawing.Color.Black, 1);

            int groupPosition = -1;
            for (int i = 0; i < thisCluster.SortedCounter; i++)
            {
                if (thisCluster.SortedGroups[i, 6] == GroupNumber)
                {
                    groupPosition = i;
                }
            }

            int ser1 = (int)thisCluster.SortedGroups[groupPosition, 2];
            int ser2 = (int)thisCluster.SortedGroups[groupPosition, 4];
            int type1 = (int)thisCluster.SortedGroups[groupPosition, 3];
            int type2 = (int)thisCluster.SortedGroups[groupPosition, 5];

            int[] groupXY = GetSerXYValue(GroupNumber, 1);
            int[] ser1XY = GetSerXYValue(ser1, type1);
            int[] ser2XY = GetSerXYValue(ser2, type2);

            graphicsObj.DrawLine(myPen, ser1XY[0], ser1XY[1], groupXY[0], ser1XY[1]);
            graphicsObj.DrawLine(myPen, ser2XY[0], ser2XY[1], groupXY[0], ser2XY[1]);
            graphicsObj.DrawLine(myPen, groupXY[0], ser1XY[1], groupXY[0], ser2XY[1]);

        }


        private void picDrawArea_Resize(object sender, EventArgs e)
        {
            graphicsObj = picDrawArea.CreateGraphics();
            this.Refresh();
            DrawTree();
        }

        // ============================   DUMP DATA TO TEXT FILES AREA  ====================

        private void dumpXYValues()
        {
            StreamWriter sw = new StreamWriter("C:\\temp\\xyvalOut.txt", true);

            for (int i = 0; i < (DrawArray.Length / 5); i++)
            {
                sw.Write(DrawArray[i, 0] + "\t" +
                    DrawArray[i, 1] + "\t" +
                    DrawArray[i, 2] + "\t" +
                    DrawArray[i, 3] + "\t" +
                    DrawArray[i, 4] + "\r\n");
            }

            sw.Close();
        }

        private void DumpGroups()
        {
            StreamWriter sw = new StreamWriter("C:\\temp\\groupsOut.txt", true);

            for (int i = 0; i < thisCluster.SortedCounter; i++)
            {
                sw.Write(thisCluster.SortedGroups[i, 0] + "\t" +
                    thisCluster.SortedGroups[i, 1] + "\t" +
                    thisCluster.SortedGroups[i, 2] + "\t" +
                    thisCluster.SortedGroups[i, 3] + "\t" +
                    thisCluster.SortedGroups[i, 4] + "\t" +
                    thisCluster.SortedGroups[i, 5] + "\t" +
                    thisCluster.SortedGroups[i, 6] + "\r\n");
            }

            sw.Close();

            StreamWriter sw2 = new StreamWriter("C:\\temp\\draworderOut.txt", true);

            for (int i = 0; i < thisCluster.DrawOrderCounter; i++)
            {
                sw2.Write(thisCluster.DrawOrder[i] + "\r\n");
            }

            sw2.Close();
        }

        // ====================================================================================================


    }
}