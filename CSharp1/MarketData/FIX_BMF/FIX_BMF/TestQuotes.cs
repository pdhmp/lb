using System;
using System.Collections.Generic;
using System.Text;

namespace FIX_BMF
{
    class TestQuotes
    {

        public static void ExecTestQuotes()
        {
            MarketDepthItem curItem = new MarketDepthItem();

            curItem.Ask_Update(1, 1779, 30);
            curItem.Ask_Update(2, 1779.5, 10);
            curItem.Ask_Update(2, 1779.5, 15);
            curItem.Ask_Update(3, 1780, 20);
            curItem.Ask_Update(2, 1779.5, 25);
            curItem.Ask_Update(3, 1780, 10);
            curItem.Ask_Update(1, 1778.5, 10);
            curItem.Ask_Update(2, 1779, 20);
            curItem.Ask_Update(3, 1779.5, 30);
            curItem.Ask_Update(4, 1780, 5);
            curItem.Ask_Update(2, 1779, 25);
            curItem.Ask_Update(3, 1779.5, 25);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(2, 1779, 20);
            curItem.Ask_Update(2, 1779, 10);
            curItem.Ask_Update(3, 1779.5, 35);
            curItem.Ask_Update(3, 1779.5, 30);
            curItem.Ask_Update(4, 1780, 10);
            curItem.Ask_Update(3, 1779.5, 20);
            curItem.Ask_Update(4, 1780, 20);
            curItem.Ask_Update(1, 1778.5, 5);
            curItem.Ask_Update(2, 1779, 20);
            curItem.Ask_Update(3, 1779.5, 15);
            curItem.Ask_Update(4, 1780, 25);
            curItem.Ask_Delete(1);
            curItem.Ask_Update(1, 1779, 25);
            curItem.Ask_Update(1, 1779, 20);
            curItem.Ask_Update(2, 1779.5, 20);
            curItem.Ask_Update(1, 1779, 30);
            curItem.Ask_Update(2, 1779.5, 10);
            curItem.Ask_Update(2, 1779.5, 15);
            curItem.Ask_Update(3, 1780, 20);
            curItem.Ask_Update(1, 1778.5, 10);
            curItem.Ask_Update(2, 1779, 20);
            curItem.Ask_Update(3, 1779.5, 25);
            curItem.Ask_Update(4, 1780, 10);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(2, 1779, 15);
            curItem.Ask_Update(3, 1779.5, 30);
            curItem.Ask_Update(4, 1780, 5);
            curItem.Ask_Update(2, 1779, 20);
            curItem.Ask_Update(3, 1779.5, 25);
            curItem.Ask_Delete(4);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(4, 1780, 5);
            curItem.Ask_Delete(4);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(4, 1780, 5);
            curItem.Ask_Delete(4);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(4, 1780, 5);
            curItem.Ask_Delete(4);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(4, 1780, 5);
            curItem.Ask_Delete(4);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(4, 1780, 5);
            curItem.Ask_Update(2, 1779, 15);
            curItem.Ask_Update(3, 1779.5, 20);
            curItem.Ask_Update(1, 1778.5, 10);
            curItem.Ask_Update(2, 1779, 10);
            curItem.Ask_Update(3, 1779.5, 30);
            curItem.Ask_Update(3, 1779.5, 25);
            curItem.Ask_Update(4, 1780, 10);
            curItem.Ask_Update(3, 1779.5, 20);
            curItem.Ask_Update(4, 1780, 20);
            curItem.Ask_Update(1, 1778.5, 5);
            curItem.Ask_Update(2, 1779, 20);
            curItem.Ask_Update(2, 1779, 25);
            curItem.Ask_Update(3, 1779.5, 10);
            curItem.Ask_Update(3, 1779.5, 15);
            curItem.Ask_Update(4, 1780, 15);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(2, 1779, 15);
            curItem.Ask_Update(3, 1779.5, 20);
            curItem.Ask_Update(4, 1780, 5);
            curItem.Ask_Update(1, 1778.5, 10);
            curItem.Ask_Update(3, 1779.5, 25);
            curItem.Ask_Update(3, 1779.5, 20);
            curItem.Ask_Update(1, 1778.5, 5);
            curItem.Ask_Update(2, 1779, 20);
            curItem.Ask_Update(1, 1778.5, 10);
            curItem.Ask_Update(1, 1778.5, 5);
            curItem.Ask_Update(1, 1778.5, 10);
            curItem.Ask_Update(2, 1779, 15);
            curItem.Ask_Delete(4);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(1, 1778.5, 10);
            curItem.Ask_Update(4, 1780, 5);
            curItem.Ask_Delete(4);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(1, 1778.5, 10);
            curItem.Ask_Update(4, 1780, 5);
            curItem.Ask_Delete(4);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(1, 1778.5, 10);
            curItem.Ask_Update(4, 1780, 5);
            curItem.Ask_Delete(4);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(2, 1779, 10);
            curItem.Ask_Update(2, 1779, 15);
            curItem.Ask_Update(3, 1779.5, 15);
            curItem.Ask_Update(1, 1778, 5);
            curItem.Ask_Update(2, 1778.5, 15);
            curItem.Ask_Update(3, 1779, 20);
            curItem.Ask_Update(4, 1779.5, 10);
            curItem.Ask_Update(1, 1778, 10);
            curItem.Ask_Update(3, 1779, 25);
            curItem.Ask_Delete(5);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(5, 1780, 5);
            curItem.Ask_Update(1, 1778, 15);
            curItem.Ask_Update(2, 1778.5, 5);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(3, 1779, 20);
            curItem.Ask_Delete(5);
            curItem.Ask_Update(2, 1778.5, 15);
            curItem.Ask_Update(5, 1784, 5);
            curItem.Ask_Update(3, 1779, 25);
            curItem.Ask_Update(4, 1779.5, 5);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(3, 1779, 35);
            curItem.Ask_Update(3, 1779, 30);
            curItem.Ask_Update(4, 1779.5, 10);
            curItem.Ask_Update(3, 1779, 20);
            curItem.Ask_Update(4, 1779.5, 20);
            curItem.Ask_Update(1, 1778, 5);
            curItem.Ask_Update(2, 1778.5, 20);
            curItem.Ask_Update(3, 1779, 15);
            curItem.Ask_Update(4, 1779.5, 25);
            curItem.Ask_Delete(1);
            curItem.Ask_Update(1, 1778.5, 25);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(2, 1779, 20);
            curItem.Ask_Update(2, 1779, 25);
            curItem.Ask_Update(3, 1779.5, 20);
            curItem.Ask_Update(1, 1778.5, 25);
            curItem.Ask_Update(2, 1779, 15);
            curItem.Ask_Update(2, 1779, 20);
            curItem.Ask_Update(3, 1779.5, 10);
            curItem.Ask_Update(1, 1778, 5);
            curItem.Ask_Update(2, 1778.5, 15);
            curItem.Ask_Update(3, 1779, 15);
            curItem.Ask_Update(4, 1779.5, 15);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(3, 1779, 25);
            curItem.Ask_Update(3, 1779, 20);
            curItem.Ask_Update(4, 1779.5, 25);
            curItem.Ask_Delete(1);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(4, 1780, 5);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(2, 1779, 15);
            curItem.Ask_Delete(5);
            curItem.Ask_Update(1, 1778, 5);
            curItem.Ask_Update(2, 1778.5, 15);
            curItem.Ask_Delete(1);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(5, 1784, 5);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(2, 1779, 20);
            curItem.Ask_Delete(4);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(4, 1780, 5);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(2, 1779, 10);
            curItem.Ask_Update(2, 1779, 15);
            curItem.Ask_Update(3, 1779.5, 20);
            curItem.Ask_Delete(5);
            curItem.Ask_Update(1, 1778, 5);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(3, 1779, 20);
            curItem.Ask_Update(4, 1779.5, 10);
            curItem.Ask_Update(2, 1778.5, 5);
            curItem.Ask_Update(3, 1779, 30);
            curItem.Ask_Update(3, 1779, 25);
            curItem.Ask_Update(4, 1779.5, 15);
            curItem.Ask_Delete(5);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(5, 1784, 5);
            curItem.Ask_Update(2, 1778.5, 15);
            curItem.Ask_Update(3, 1779, 20);
            curItem.Ask_Update(1, 1778, 10);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(1, 1778, 5);
            curItem.Ask_Update(2, 1778.5, 20);
            curItem.Ask_Update(3, 1779, 15);
            curItem.Ask_Update(4, 1779.5, 25);
            curItem.Ask_Delete(5);
            curItem.Ask_Update(2, 1778.5, 15);
            curItem.Ask_Update(5, 1780, 5);
            curItem.Ask_Delete(1);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(5, 1784, 5);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(2, 1779, 20);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(2, 1779, 10);
            curItem.Ask_Update(2, 1779, 15);
            curItem.Ask_Update(3, 1779.5, 20);
            curItem.Ask_Delete(5);
            curItem.Ask_Update(1, 1778, 5);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(3, 1779, 20);
            curItem.Ask_Update(4, 1779.5, 10);
            curItem.Ask_Update(3, 1779, 25);
            curItem.Ask_Update(4, 1779.5, 5);
            curItem.Ask_Update(1, 1778, 10);
            curItem.Ask_Update(2, 1778.5, 5);
            curItem.Ask_Delete(5);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(5, 1784, 5);
            curItem.Ask_Update(2, 1778.5, 5);
            curItem.Ask_Update(3, 1779, 35);
            curItem.Ask_Update(3, 1779, 30);
            curItem.Ask_Update(4, 1779.5, 10);
            curItem.Ask_Update(3, 1779, 25);
            curItem.Ask_Update(4, 1779.5, 20);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(3, 1779, 20);
            curItem.Ask_Update(1, 1778, 5);
            curItem.Ask_Update(2, 1778.5, 20);
            curItem.Ask_Delete(1);
            curItem.Ask_Update(1, 1778.5, 25);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(4, 1780, 5);
            curItem.Ask_Update(2, 1779, 15);
            curItem.Ask_Update(3, 1779.5, 25);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(2, 1779, 20);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(2, 1779, 10);
            curItem.Ask_Update(2, 1779, 15);
            curItem.Ask_Update(3, 1779.5, 20);
            curItem.Ask_Delete(5);
            curItem.Ask_Update(1, 1778, 5);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(3, 1779, 20);
            curItem.Ask_Update(4, 1779.5, 10);
            curItem.Ask_Update(1, 1778, 10);
            curItem.Ask_Update(2, 1778.5, 5);
            curItem.Ask_Delete(5);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(5, 1784, 5);
            curItem.Ask_Update(2, 1778.5, 15);
            curItem.Ask_Update(3, 1779, 15);
            curItem.Ask_Update(3, 1779, 20);
            curItem.Ask_Update(4, 1779.5, 5);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(3, 1779, 30);
            curItem.Ask_Update(3, 1779, 25);
            curItem.Ask_Update(4, 1779.5, 10);
            curItem.Ask_Update(3, 1779, 20);
            curItem.Ask_Update(4, 1779.5, 20);
            curItem.Ask_Update(1, 1778, 5);
            curItem.Ask_Update(2, 1778.5, 20);
            curItem.Ask_Delete(1);
            curItem.Ask_Update(1, 1778.5, 25);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(4, 1780, 5);
            curItem.Ask_Update(2, 1779, 15);
            curItem.Ask_Update(3, 1779.5, 25);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(2, 1779, 20);
            curItem.Ask_Delete(4);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(4, 1780, 5);
            curItem.Ask_Delete(4);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(1, 1778.5, 15);
            curItem.Ask_Update(4, 1780, 5);
            curItem.Ask_Update(1, 1778.5, 20);
            curItem.Ask_Update(2, 1779, 10);
            curItem.Ask_Update(2, 1779, 15);
            curItem.Ask_Update(3, 1779.5, 20);
            curItem.Ask_Delete(5);
            curItem.Ask_Update(1, 1778, 5);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(3, 1779, 20);
            curItem.Ask_Update(4, 1779.5, 10);
            curItem.Ask_Update(1, 1778, 10);
            curItem.Ask_Update(2, 1778.5, 5);
            curItem.Ask_Delete(5);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(5, 1784, 5);
            curItem.Ask_Update(2, 1778.5, 15);
            curItem.Ask_Update(3, 1779, 15);
            curItem.Ask_Update(3, 1779, 20);
            curItem.Ask_Update(4, 1779.5, 5);
            curItem.Ask_Update(1, 1778, 5);
            curItem.Ask_Update(2, 1778.5, 20);
            curItem.Ask_Update(1, 1778, 10);
            curItem.Ask_Update(2, 1778.5, 15);
            curItem.Ask_Update(1, 1778, 20);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(2, 1778.5, 15);
            curItem.Ask_Update(3, 1779, 15);
            curItem.Ask_Update(2, 1778.5, 25);
            curItem.Ask_Update(3, 1779, 10);
            curItem.Ask_Delete(5);
            curItem.Ask_Update(1, 1777.5, 10);
            curItem.Ask_Update(2, 1778, 15);
            curItem.Ask_Delete(5);
            curItem.Ask_Update(2, 1778, 20);
            curItem.Ask_Update(5, 1784, 5);
            curItem.Ask_Update(1, 1777.5, 15);
            curItem.Ask_Update(2, 1778, 15);
            curItem.Ask_Update(2, 1778, 20);
            curItem.Ask_Update(3, 1778.5, 20);
            curItem.Ask_Update(3, 1778.5, 25);
            curItem.Ask_Update(4, 1779, 5);
            curItem.Ask_Update(3, 1778.5, 15);
            curItem.Ask_Update(4, 1779, 15);
            curItem.Ask_Update(1, 1777.5, 5);
            curItem.Ask_Update(2, 1778, 30);
            curItem.Ask_Update(3, 1778.5, 10);
            curItem.Ask_Update(4, 1779, 20);
            curItem.Ask_Update(2, 1778, 20);
            curItem.Ask_Update(3, 1778.5, 20);
            curItem.Ask_Update(1, 1777.5, 10);
            curItem.Ask_Update(2, 1778, 10);
            curItem.Ask_Update(3, 1778.5, 25);
            curItem.Ask_Update(4, 1779, 10);
            curItem.Ask_Update(3, 1778.5, 30);
            curItem.Ask_Update(4, 1779, 5);
            curItem.Ask_Update(2, 1778, 15);
            curItem.Ask_Update(3, 1778.5, 20);
            curItem.Ask_Delete(5);
            curItem.Ask_Update(2, 1778, 10);
            curItem.Ask_Update(5, 1779.5, 5);
            curItem.Ask_Update(2, 1778, 5);
            curItem.Ask_Update(3, 1778.5, 30);
            curItem.Ask_Update(3, 1778.5, 25);
            curItem.Ask_Update(4, 1779, 10);
            curItem.Ask_Update(1, 1777.5, 5);
            curItem.Ask_Update(2, 1778, 15);
            curItem.Ask_Delete(1);
            curItem.Ask_Update(1, 1778, 20);
            curItem.Ask_Update(5, 1784, 5);
            curItem.Ask_Delete(4);
            curItem.Ask_Update(1, 1778, 25);
            curItem.Ask_Update(1, 1778, 20);
            curItem.Ask_Update(2, 1778.5, 30);
            curItem.Ask_Update(2, 1778.5, 25);
            curItem.Ask_Update(3, 1779, 20);
            curItem.Ask_Update(2, 1778.5, 20);
            curItem.Ask_Update(3, 1779, 25);
            curItem.Ask_Update(1, 1778, 15);
            curItem.Ask_Update(4, 1779.5, 5);
            curItem.Ask_Update(1, 1778, 25);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(2, 1778.5, 15);
            curItem.Ask_Update(3, 1779, 20);
            curItem.Ask_Update(2, 1778.5, 25);
            curItem.Ask_Update(3, 1779, 10);
            curItem.Ask_Delete(5);
            curItem.Ask_Update(1, 1777.5, 10);
            curItem.Ask_Update(2, 1778, 15);
            curItem.Ask_Update(3, 1778.5, 30);
            curItem.Ask_Update(4, 1779, 5);
            curItem.Ask_Update(1, 1777.5, 15);
            curItem.Ask_Update(2, 1778, 10);
            curItem.Ask_Delete(5);
            curItem.Ask_Update(2, 1778, 15);
            curItem.Ask_Update(5, 1784, 5);
            curItem.Ask_Update(2, 1778, 20);
            curItem.Ask_Update(3, 1778.5, 25);
            curItem.Ask_Update(2, 1778, 10);
            curItem.Ask_Update(3, 1778.5, 35);
            curItem.Ask_Update(3, 1778.5, 30);
            curItem.Ask_Update(4, 1779, 10);
            curItem.Ask_Update(3, 1778.5, 20);
            curItem.Ask_Update(4, 1779, 20);
            curItem.Ask_Update(1, 1777.5, 5);
            curItem.Ask_Update(2, 1778, 20);
            curItem.Ask_Delete(1);
            curItem.Ask_Update(1, 1778, 25);
            curItem.Ask_Update(2, 1778.5, 15);
            curItem.Ask_Update(3, 1779, 25);
            curItem.Ask_Update(1, 1778, 20);
            curItem.Ask_Update(2, 1778.5, 20);
            curItem.Ask_Update(1, 1778, 30);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(2, 1778.5, 15);
            curItem.Ask_Update(3, 1779, 20);
            curItem.Ask_Update(1, 1777.5, 10);
            curItem.Ask_Update(2, 1778, 20);
            curItem.Ask_Update(3, 1778.5, 25);
            curItem.Ask_Update(4, 1779, 10);
            curItem.Ask_Update(1, 1777.5, 5);
            curItem.Ask_Update(3, 1778.5, 20);
            curItem.Ask_Update(2, 1778, 15);
            curItem.Ask_Update(1, 1777.5, 10);
            curItem.Ask_Update(2, 1778, 10);
            curItem.Ask_Update(3, 1778.5, 25);
            curItem.Ask_Update(4, 1779, 5);
            curItem.Ask_Update(2, 1778, 15);
            curItem.Ask_Update(3, 1778.5, 20);
            curItem.Ask_Update(1, 1777.5, 5);
            curItem.Ask_Update(2, 1778, 20);
            curItem.Ask_Update(1, 1777.5, 10);
            curItem.Ask_Update(2, 1778, 15);
            curItem.Ask_Update(1, 1777.5, 5);
            curItem.Ask_Update(2, 1778, 10);
            curItem.Ask_Update(3, 1778.5, 30);
            curItem.Ask_Update(3, 1778.5, 25);
            curItem.Ask_Update(4, 1779, 10);
            curItem.Ask_Update(3, 1778.5, 20);
            curItem.Ask_Update(4, 1779, 20);
            curItem.Ask_Delete(1);
            curItem.Ask_Update(1, 1778, 15);
            curItem.Ask_Update(2, 1778.5, 15);
            curItem.Ask_Update(3, 1779, 25);
            curItem.Ask_Update(1, 1778, 10);
            curItem.Ask_Update(2, 1778.5, 20);
            curItem.Ask_Update(1, 1778, 20);
            curItem.Ask_Update(2, 1778.5, 10);
            curItem.Ask_Update(2, 1778.5, 15); PrintDepth(curItem);
            curItem.Ask_Update(3, 1779, 20);
            curItem.Ask_Update(2, 1778.5, 25);
            curItem.Ask_Update(3, 1779, 10);
            curItem.Ask_Update(1, 1777.5, 5);
            curItem.Ask_Update(2, 1778, 15);
            curItem.Ask_Update(3, 1778.5, 30);
            curItem.Ask_Update(4, 1779, 5);
            curItem.Ask_Update(2, 1778, 20);
            curItem.Ask_Update(3, 1778.5, 25);

            PrintDepth(curItem);

            
        }

        public static void PrintDepth(MarketDepthItem curItem)
        {
            GlobalVars.Instance.AddMessage(curItem.Ticker + "Ask1" + "\t\t" + curItem.AskSize1.ToString("0") + '\t' + curItem.Ask1.ToString("0.00"));
            GlobalVars.Instance.AddMessage(curItem.Ticker + "Ask2" + "\t\t" + curItem.AskSize2.ToString("0") + '\t' + curItem.Ask2.ToString("0.00"));
            GlobalVars.Instance.AddMessage(curItem.Ticker + "Ask3" + "\t\t" + curItem.AskSize3.ToString("0") + '\t' + curItem.Ask3.ToString("0.00"));
            GlobalVars.Instance.AddMessage(curItem.Ticker + "Ask4" + "\t\t" + curItem.AskSize4.ToString("0") + '\t' + curItem.Ask4.ToString("0.00"));
            GlobalVars.Instance.AddMessage(curItem.Ticker + "Ask5" + "\t\t" + curItem.AskSize5.ToString("0") + '\t' + curItem.Ask5.ToString("0.00"));
            GlobalVars.Instance.AddMessage("============");

        }

    }
}