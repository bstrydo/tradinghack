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
        public void Optimise_StockGoingUp_LongStock(string symbol, double historicPrice, double futurePrice)
        {
            List<HistoricPrice> historicPrices = new List<HistoricPrice>();
            historicPrices.Add(new HistoricPrice(symbol, historicPrice));
            List<ForecastPrice> forecastPrices = new List<ForecastPrice>();
            forecastPrices.Add(new ForecastPrice(symbol, futurePrice));
            PortfolioOptimiser portfolioOptimiser = new PortfolioOptimiser(historicPrices, forecastPrices);

            portfolioOptimiser.Optimise();

            Assert.Equal(portfolioOptimiser.Portfolio, new Dictionary<string, double>() { { symbol, 1 } });
        }

        [Fact]
        public void Optimise_StocksGoingUp_LongStocks()
        {
            List<HistoricPrice> historicPrices = new List<HistoricPrice>();
            historicPrices.Add(new HistoricPrice("AAPL", 143.17));
            historicPrices.Add(new HistoricPrice("GOOGL", 824.17));
            List<ForecastPrice> forecastPrices = new List<ForecastPrice>();
            forecastPrices.Add(new ForecastPrice("AAPL", 146.03));
            forecastPrices.Add(new ForecastPrice("GOOGL", 840.65));
            PortfolioOptimiser portfolioOptimiser = new PortfolioOptimiser(historicPrices, forecastPrices);

            portfolioOptimiser.Optimise();

            var changeInApple = (146.03 / 143.17 - 1);
            var changeInGoogle = (840.65 / 824.17 - 1);
            var totalChange = changeInApple + changeInGoogle;
            Assert.Equal(portfolioOptimiser.Portfolio, new Dictionary<string, double>() { { "AAPL", changeInApple / totalChange }, { "GOOGL", changeInGoogle / totalChange } });
        }

        [Fact]
        public void Portfolio_RetrieveBeforeOptimise_ShouldThrowInvalidOperationException()
        {

            List<HistoricPrice> historicPrices = new List<HistoricPrice>();
            historicPrices.Add(new HistoricPrice("AAPL", 143.17));
            List<ForecastPrice> forecastPrices = new List<ForecastPrice>();
            forecastPrices.Add(new ForecastPrice("AAPL", 146.03));
            PortfolioOptimiser portfolioOptimiser = new PortfolioOptimiser(historicPrices, forecastPrices);

            var exception = Assert.Throws<InvalidOperationException>(() => portfolioOptimiser.Portfolio);
            Assert.Equal("Can't retrieve portfolio before optimising", exception.Message);
        }

        [Fact]
        public void Optimise_StocksGoingUpAnyOrder_LongStocks()
        {
            List<HistoricPrice> historicPrices = new List<HistoricPrice>();
            historicPrices.Add(new HistoricPrice("GOOGL", 824.17));
            historicPrices.Add(new HistoricPrice("AAPL", 143.17));
            List<ForecastPrice> forecastPrices = new List<ForecastPrice>();
            forecastPrices.Add(new ForecastPrice("AAPL", 146.03));
            forecastPrices.Add(new ForecastPrice("GOOGL", 840.65));
            PortfolioOptimiser portfolioOptimiser = new PortfolioOptimiser(historicPrices, forecastPrices);

            portfolioOptimiser.Optimise();

            var changeInApple = (146.03 / 143.17 - 1);
            var changeInGoogle = (840.65 / 824.17 - 1);
            var totalChange = changeInApple + changeInGoogle;
            Assert.Equal(portfolioOptimiser.Portfolio, new Dictionary<string, double>() { { "AAPL", changeInApple / totalChange }, { "GOOGL", changeInGoogle / totalChange } });
        }
    }
}
