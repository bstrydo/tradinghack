using System;
using System.Collections.Generic;
using Xunit;

namespace Trader
{
    public class PortfolioOptimiserTests
    {
        [Theory]
        [InlineData("AAPL", 143.17, 145.23)]
        [InlineData("GOOGL", 824.17, 824.73)]
        public void Optimise_StockGoingUp_LongStock(string symbol, double historicPrice, double forecastPrice)
        {
            var historicPrices = new Dictionary<string, List<HistoricPrice>>()
                {{ symbol, new List<HistoricPrice> { new HistoricPrice(new DateTime(2017,3,1), historicPrice) }}};
            var forecastPrices = new Dictionary<string, List<ForecastPrice>>()
                {{ symbol, new List<ForecastPrice> { new ForecastPrice(forecastPrice) }}};
            PortfolioOptimiser portfolioOptimiser = new PortfolioOptimiser(historicPrices, forecastPrices);

            portfolioOptimiser.Optimise();

            Assert.Equal(portfolioOptimiser.Portfolio, new Dictionary<string, double>() { { symbol, 1 } });
        }

        [Fact]
        public void Optimise_StocksGoingUp_LongStocks()
        {
            var historicPrices = new Dictionary<string, List<HistoricPrice>>()
                {
                    { "GOOGL", new List<HistoricPrice> { new HistoricPrice(new DateTime(2017,3,1), 824.17) } },
                    { "AAPL", new List<HistoricPrice> { new HistoricPrice(new DateTime(2017,3,1), 143.17) } }
                };
            var forecastPrices = new Dictionary<string, List<ForecastPrice>>()
                {
                    { "GOOGL", new List<ForecastPrice> { new ForecastPrice(840.65) } },
                    { "AAPL", new List<ForecastPrice> { new ForecastPrice(146.03) } }
                };
            PortfolioOptimiser portfolioOptimiser = new PortfolioOptimiser(historicPrices, forecastPrices);

            portfolioOptimiser.Optimise();

            var changeInApple = (146.03 / 143.17 - 1);
            var changeInGoogle = (840.65 / 824.17 - 1);
            var totalChange = changeInApple + changeInGoogle;
            Assert.Equal(portfolioOptimiser.Portfolio, new Dictionary<string, double>() { { "AAPL", changeInApple / totalChange }, { "GOOGL", changeInGoogle / totalChange } });
        }

        [Fact]
        public void Get_Profile_BeforeOptimise_ShouldThrowInvalidOperationException()
        {
            var historicPrices = new Dictionary<string, List<HistoricPrice>>()
                {{ "AAPL", new List<HistoricPrice> { new HistoricPrice(new DateTime(2017,3,1), 143.17) }}};
            var forecastPrices = new Dictionary<string, List<ForecastPrice>>()
                {{ "AAPL", new List<ForecastPrice> { new ForecastPrice(146.03) }}};
            PortfolioOptimiser portfolioOptimiser = new PortfolioOptimiser(historicPrices, forecastPrices);

            var exception = Assert.Throws<InvalidOperationException>(() => portfolioOptimiser.Portfolio);
            Assert.Equal("Can't retrieve portfolio before optimising", exception.Message);
        }

        [Fact]
        public void Optimise_MultipleHistoricValuesGoingUp_LongStock()
        {
            var historicPrices = new Dictionary<string, List<HistoricPrice>>()
                {{ "AAPL", new List<HistoricPrice> { new HistoricPrice(new DateTime(2017,3,1), 146.01), new HistoricPrice(new DateTime(2017,3,2), 143.17) }}};
            var forecastPrices = new Dictionary<string, List<ForecastPrice>>()
                {{ "AAPL", new List<ForecastPrice> { new ForecastPrice(145.23) }}};
            PortfolioOptimiser portfolioOptimiser = new PortfolioOptimiser(historicPrices, forecastPrices);

            portfolioOptimiser.Optimise();

            Assert.Equal(new Dictionary<string, double>() { { "AAPL", 1 } }, portfolioOptimiser.Portfolio);
        }
    }
}
