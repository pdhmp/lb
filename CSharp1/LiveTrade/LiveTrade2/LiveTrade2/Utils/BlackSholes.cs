using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveTrade2
{
	/// <summary>
	/// Summary description for BlackSholes.
	/// </summary>
	public class BlackSholes
	{
		public BlackSholes()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        public double ImpliedVol(string CallPutFlag, double S, double X, double T, double r, double TargetPrice)
        {
            double High = 10;
            double Low = 0;
            double TestVol = 0;
            double TestPrice = 0;
            int MaxIterations = 100;
            int curIteration = 0;

            double Intrinsic = S - X;


            if (T > 0 && S > 0 && TargetPrice > Intrinsic)
            {
                while (High - Low > 0.0001)
                {
                    TestVol = (High + Low) / 2;
                    TestPrice = BlackScholes(CallPutFlag, S, X, T, r, TestVol);
                    if (TestPrice > TargetPrice)
                    {
                        High = TestVol;
                    }
                    else
                    {
                        Low = TestVol;
                    }
                    curIteration++;
                    if (curIteration > MaxIterations)
                    {
                        TestVol = 0;
                        break;
                    }
                }

                TestVol = (High + Low) / 2;
            }

            if (TestVol > 0 && TestVol < 0.001)
            { 
            }

            return TestVol;
        }

		public double BlackScholes(string CallPutFlag, double S, double X, double T, double r, double v)
		{
			double d1 = 0.0;
			double d2 = 0.0;
			double dBlackScholes = 0.0;
			
			d1 = (Math.Log(S / X) + (r + v * v / 2.0) * T) / (v * Math.Sqrt(T));
			d2 = d1 - v * Math.Sqrt(T);
			if (CallPutFlag == "Call")
			{				
				dBlackScholes = S * CND(d1) - X * Math.Exp(-r * T) * CND(d2);
			}
			else if (CallPutFlag == "Put") 
			{
				dBlackScholes = X * Math.Exp(-r * T) * CND(-d2) - S * CND(-d1);				
			}
			return dBlackScholes;
		}

        public double Delta(string CallPutFlag, double S, double X, double T, double r, double v)
        {
            double d1 = 0.0;
            double d2 = 0.0;
            double dDelta = 0.0;

            d1 = (Math.Log(S / X) + (r + v * v / 2.0) * T) / (v * Math.Sqrt(T));
            d2 = d1 - v * Math.Sqrt(T);
            if (CallPutFlag == "Call")
            {
                dDelta = CND(d1);
            }
            else if (CallPutFlag == "Put")
            {
                dDelta = CND(d1) - 1;
            }
            return dDelta;
        }

		public double CND(double X)
		{
			double L = 0.0;
			double K = 0.0;
			double dCND = 0.0;
			const double a1 = 0.31938153; 
			const double a2 = -0.356563782; 
			const double a3 = 1.781477937;
			const double a4 = -1.821255978;
			const double a5 = 1.330274429;

			L = Math.Abs(X);
			K = 1.0 / (1.0 + 0.2316419 * L);
			dCND = 1.0 - 1.0 / Math.Sqrt(2 * Convert.ToDouble(Math.PI.ToString())) * 
				Math.Exp(-L * L / 2.0) * (a1 * K + a2 * K  * K + a3 * Math.Pow(K, 3.0) + 
				a4 * Math.Pow(K, 4.0) + a5 * Math.Pow(K, 5.0));
			
			if (X < 0) 
			{
				return 1.0 - dCND;
			}
			else
			{
				return dCND;
			}
		}
	}
}

