using Xunit;
using System.Collections.Generic;

namespace Trader
{
   public class PortfolioOptimiserTests
   {
       [Theory]
       [InlineData("AAPL", 143.17, 145.23)]
       [InlineData("GOOGL", 824.17, 824.73)]
       public void Optimise_StockGoingUp_BuyStock(string symbol, double historicPrice, double futurePrice)
       {
           Dictionary<string, int> currentStockQuantities = new Dictionary<string, int>() {{symbol, 0}};
           List<HistoricPrice> historicPrices = new List<HistoricPrice>();
           historicPrices.Add(new HistoricPrice(historicPrice));
           List<ForecastPrice> forecastPrices = new List<ForecastPrice>();
           forecastPrices.Add(new ForecastPrice(futurePrice));
           PortfolioOptimiser portfolioOptimiser = new PortfolioOptimiser(currentStockQuantities, historicPrices, forecastPrices);

           portfolioOptimiser.Optimise();

           Assert.Equal(portfolioOptimiser.Profile, new Dictionary<string, double>() {{symbol,1}});
       }
   }
}
