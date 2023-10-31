using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveBook
{
    class ForwardMatcher
    {
        List<Security> SpotListCopy = new List<Security>();
        List<Security> SpotList = new List<Security>();
        List<Security> FowardList = new List<Security>();
        List<Simulation> Simulations = new List<Simulation>();

        public ForwardMatcher(List<Security> SpotList, List<Security> FwdList)
        {
            this.SpotList = SpotList;

            foreach (Security copyItem in SpotList) { SpotListCopy.Add(copyItem); }

            this.FowardList = FwdList;
        }

        public Simulation GetBestSimulation()
        {

            GenerateSimulations(new List<Combination>(), "", SpotListCopy, FowardList);

            List<Simulation> SimulationsToDelete = new List<Simulation>();

            foreach (Simulation curSimulation in Simulations)
            {
                if (!curSimulation.isValid()) SimulationsToDelete.Add(curSimulation);
            }

            foreach (Simulation curSimulation in SimulationsToDelete)
            {
                Simulations.Remove(curSimulation);
            }

            double MinDeviation = double.MaxValue;
            Simulation BestSimulation = null;

            foreach (Simulation curSimulation in Simulations)
            {
                double curDeviation = curSimulation.getDesvioPreco();
                if (curDeviation < MinDeviation)
                {
                    BestSimulation = curSimulation;
                    MinDeviation = curDeviation;
                }

            }
            //Console.WriteLine();
            return BestSimulation;
        }


        private void GenerateSimulations(List<Combination> _OriginalCombination, string _Path, List<Security> _SpotItems, List<Security> _FowardItems)
        {
            string curPath = "";

            Security curVista = _SpotItems[0];
            _SpotItems.RemoveAt(0);

            // List<string> ReturnItems = new List<string>();

            foreach (Security curTermo in _FowardItems)
            {
                curPath = _Path + curTermo.Identifier + curVista.Identifier;

                List<Combination> curPathComb = new List<Combination>();
                foreach (Combination copyItem in _OriginalCombination) { curPathComb.Add(copyItem); }
                Combination newcomb = new Combination(curTermo, curVista);
                curPathComb.Add(newcomb);

                //Console.WriteLine(curPath);
                if (_SpotItems.Count > 0)
                {
                    List<Security> SendItems = new List<Security>();
                    foreach (Security copyItem in _SpotItems) { SendItems.Add(copyItem); }

                    List<Combination> SendPathComb = new List<Combination>();
                    foreach (Combination copyItem in curPathComb) { SendPathComb.Add(copyItem); }

                    GenerateSimulations(SendPathComb, curPath, SendItems, _FowardItems);
                }
                else
                {
                    Simulation curSimulation = new Simulation(_SpotItems, _FowardItems);
                    foreach (Combination curComb in curPathComb)
                    {
                        curSimulation.CombinationList.Add(curComb);
                    }
                    Simulations.Add(curSimulation);
                }
            }
        }
    }

    public class Simulation
    {
        public List<Combination> CombinationList = new List<Combination>();
        List<Security> _ListaVista = new List<Security>();
        List<Security> _ListaTermo = new List<Security>();

        public Simulation(List<Security> ListaVista, List<Security> ListaTermo)
        {
            _ListaVista = ListaVista;
            _ListaTermo = ListaTermo;
        }

        public double getDesvioPreco()
        {
            double TotalDev = 0;

            foreach (Security curTermo in _ListaTermo)
            {
                double TotalFin = 0;
                int TotalCount = 0;

                //foreach (Combination curCombination in CombList)
                //{
                //    if (curCombination.curTermo.Identifier == curTermo.Identifier)
                //    {
                //        TotalFin = TotalFin + curCombination.curVista.Quantity * curCombination.curVista.Price;
                //        TotalCount++;
                //    }
                //}

                //double avPrice = TotalFin / TotalCount;

                //double TotalSquare = 0;
                //double TotalQuantity = 0;

                //foreach (Combination curCombination in CombList)
                //{
                //    if (curCombination.curTermo.Identifier == curTermo.Identifier)
                //    {
                //        TotalSquare = TotalFin + curCombination.curVista.Quantity * Math.Pow(curCombination.curVista.Price - avPrice, 2);
                //        TotalQuantity = TotalQuantity + curCombination.curVista.Quantity;
                //        TotalCount++;
                //    }
                //}


                double TotalSquare = 0;
                double TotalQuantity = 0;

                foreach (Combination curCombination in CombinationList)
                {
                    if (curCombination.curTermo.Identifier == curTermo.Identifier)
                    {
                        TotalSquare = TotalFin + curCombination.curVista.Quantity * Math.Pow(-curCombination.curVista.Price - curTermo.Price, 2);
                        TotalQuantity = TotalQuantity + curCombination.curVista.Quantity;
                        TotalCount++;
                    }
                }

                TotalDev = TotalDev + TotalSquare / TotalQuantity;
            }

            return TotalDev;
        }

        public bool isValid()
        {
            List<Combination> CombinationsToDelete = new List<Combination>();

            foreach (Security curTermo in _ListaTermo)
            {
                double AlocQuantity = 0;
                foreach (Combination curCombination in CombinationList)
                {
                    if (curCombination.curTermo.Identifier == curTermo.Identifier)
                    {
                        AlocQuantity = AlocQuantity + curCombination.curVista.Quantity;

                        if (curTermo.Quantity == AlocQuantity) { curCombination.curVista.Quantity = AlocQuantity; }
                        else { CombinationsToDelete.Add(curCombination); }
                    }
                }
            }

            foreach (Combination combToDelete in CombinationsToDelete) { CombinationList.Remove(combToDelete); }


            if (CombinationList.Count == _ListaTermo.Count)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
    public class Combination
    {
        public Combination(Security _curTermo, Security _curVista)
        {
            curTermo = _curTermo;
            curVista = _curVista;
        }

        public Security curTermo;
        public Security curVista;

        public override string ToString()
        {
            return curVista.Identifier + curTermo.Identifier;
        }
    }
}
