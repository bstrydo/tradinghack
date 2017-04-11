using System.Collections.Generic;

namespace Trader
{
   public class PortfolioOptimiser
   {
       private Dictionary<string, int> currentStockQuantities;
       private Dictionary<string, double> portfolio;

       public PortfolioOptimiser(Dictionary<string, int> currentStockQuantities, List<HistoricPrice> historicPrices, List<ForecastPrice> forecastPrices)
       {
           this.currentStockQuantities = currentStockQuantities;
       }

       public void Optimise()
       {
           foreach(var stock in currentStockQuantities.Keys)
           {
               portfolio = new Dictionary<string, double>() {{stock, 1.0}};
           }
       }

       public Dictionary<string, double> Profile { get { return portfolio; } }
   }
}
