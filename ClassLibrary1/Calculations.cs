using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockRoutines
{
    public class Calculations
    {

        //http://investexcel.net/how-to-calculate-if-a-stock-is-undervalued-or-overvalued/
        public static bool ValuationFormula(float sharePrice, float dividendPerShare, float EPS, float PE,
            float ForwardPE, float epsGrowth)
        {
            return (sharePrice < ValuationFormulaCalc(sharePrice, dividendPerShare, EPS, PE, ForwardPE, epsGrowth));
        }


        //share Price : current stock price
        // dividend per share : annual divident per share, or divident Yield * stock price
        // EPS = trailing 12 month EPS something like 9.69
        // Forward PE Assumption = i guess just use PE if forward is not available
        // EPS Growth = past 5 year growth
        public static float ValuationFormulaCalc(float sharePrice, float dividendPerShare, float EPS, float PE,
            float ForwardPE, float epsGrowth)
        {
            float year1 = EPS * (1 + epsGrowth);
            float year2 = year1 * (1 + epsGrowth);
            float year3 = year2 * (1 + epsGrowth);

            float totalEPS = year1 + year2 + year3;

            float expShareYear3 = year3 * ForwardPE;
            float dividentPayoutRatio = dividendPerShare / year3;

            //total payout over 3 years
            float totalDiv = dividentPayoutRatio * totalEPS;

            float expectedShareYear3 = totalDiv + expShareYear3;


            //assuming a 10% rate of return desired
            float goodValue = expShareYear3 / (float) Math.Pow(1.1, 3);

            return goodValue;
        }



    }
}
