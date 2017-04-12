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
            Dictionary<string, int> currentStockQuantities = new Dictionary<string, int>() { { symbol, 0 } };
            List<HistoricPrice> historicPrices = new List<HistoricPrice>();
            historicPrices.Add(new HistoricPrice(symbol, historicPrice));
            List<ForecastPrice> forecastPrices = new List<ForecastPrice>();
            forecastPrices.Add(new ForecastPrice(symbol, futurePrice));
            PortfolioOptimiser portfolioOptimiser = new PortfolioOptimiser(currentStockQuantities, historicPrices, forecastPrices);

            portfolioOptimiser.Optimise();

            Assert.Equal(portfolioOptimiser.Profile, new Dictionary<string, double>() { { symbol, 1 } });
        }

        [Fact]
        public void Optimise_Stocks_GoingUp_LongStocks()
        {
            Dictionary<string, int> currentStockQuantities = new Dictionary<string, int>() { { "AAPL", 0 }, { "GOOGL", 0 } };
            List<HistoricPrice> historicPrices = new List<HistoricPrice>();
            historicPrices.Add(new HistoricPrice("AAPL", 143.17));
            historicPrices.Add(new HistoricPrice("GOOGL", 824.17));
            List<ForecastPrice> forecastPrices = new List<ForecastPrice>();
            forecastPrices.Add(new ForecastPrice("AAPL", 146.03));
            forecastPrices.Add(new ForecastPrice("GOOGL", 840.65));
            PortfolioOptimiser portfolioOptimiser = new PortfolioOptimiser(currentStockQuantities, historicPrices, forecastPrices);

            portfolioOptimiser.Optimise();

            var changeInApple = (146.03 / 143.17 - 1);
            var changeInGoogle = (840.65 / 824.17 - 1);
            var totalChange = changeInApple + changeInGoogle;
            Assert.Equal(portfolioOptimiser.Profile, new Dictionary<string, double>() { { "AAPL", changeInApple / totalChange }, { "GOOGL", changeInGoogle / totalChange } });
        }
    }
}
