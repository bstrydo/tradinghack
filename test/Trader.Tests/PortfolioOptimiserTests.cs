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
            var historicPrices = new Dictionary<string, List<double>>()
                {{ symbol, new List<double>{ historicPrice }}};
            var forecastPrices = new Dictionary<string, List<double>>()
                {{ symbol, new List<double>{ futurePrice }}};
            PortfolioOptimiser portfolioOptimiser = new PortfolioOptimiser(historicPrices, forecastPrices);

            portfolioOptimiser.Optimise();

            Assert.Equal(portfolioOptimiser.Portfolio, new Dictionary<string, double>() { { symbol, 1 } });
        }

        [Fact]
        public void Optimise_StocksGoingUp_LongStocks()
        {
            var historicPrices = new Dictionary<string, List<double>>()
                {
                    { "GOOGL", new List<double> { 824.17 } },
                    { "AAPL", new List<double> { 143.17 } }
                };
            var forecastPrices = new Dictionary<string, List<double>>()
                {
                    { "GOOGL", new List<double> { 840.65 } },
                    { "AAPL", new List<double> { 146.03 } }
                };
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
            var historicPrices = new Dictionary<string, List<double>>()
                {{ "AAPL", new List<double>{ 143.17 }}};
            var forecastPrices = new Dictionary<string, List<double>>()
                {{ "AAPL", new List<double>{ 146.03 }}};
            PortfolioOptimiser portfolioOptimiser = new PortfolioOptimiser(historicPrices, forecastPrices);

            var exception = Assert.Throws<InvalidOperationException>(() => portfolioOptimiser.Portfolio);
            Assert.Equal("Can't retrieve portfolio before optimising", exception.Message);
        }
    }
}
