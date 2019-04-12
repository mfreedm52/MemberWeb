using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp
{
    

    class Program
    {
        static void Main(string[] args)
        {
            ////MSFT
            //float price = 57.89f;
            //float dividendPerShare = 1.44f; 
            //float EPS = 2.76f;
            //float PE = 27.57f;
            //float ForwardPE = 17.98f;
            //float epsGrowth = 0.0773f;

            //DISH

            float price = 30.68f;
            float dividendPerShare = 1.04f;
            float EPS = 2.36f;
            float PE = 13.07f;
            float ForwardPE = 13.07f;
            float epsGrowth = 0.0387f;

            var d = StockRoutines.Calculations.ValuationFormulaCalc(price, dividendPerShare, EPS, PE, ForwardPE, epsGrowth);

            Console.WriteLine("stock value: " + d);
            

        }
    }
}
