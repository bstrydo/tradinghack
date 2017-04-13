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
            Stock stock = new Stock(symbol, new List<HistoricPrice>() { new HistoricPrice(new DateTime(2017,3,1), historicPrice) }, new ForecastPrice(forecastPrice));
            PortfolioOptimiser portfolioOptimiser = new PortfolioOptimiser(new List<Stock>() { stock });

            portfolioOptimiser.Optimise();

            Assert.Equal(portfolioOptimiser.Portfolio, new Dictionary<string, double>() { { symbol, 1 } });
        }


        [Fact]
        public void Optimise_StocksGoingUp_LongStocks()
        {
            var stocks = new List<Stock>()
            {
                { new Stock("GOOGL", new List<HistoricPrice>() { new HistoricPrice(new DateTime(2017,3,1), 824.17) }, new ForecastPrice(840.65)) },
                { new Stock("AAPL", new List<HistoricPrice>() { new HistoricPrice(new DateTime(2017,3,1), 143.17) }, new ForecastPrice(146.03)) }
            };
            PortfolioOptimiser portfolioOptimiser = new PortfolioOptimiser(stocks);

            portfolioOptimiser.Optimise();

            var changeInApple = (146.03 / 143.17 - 1);
            var changeInGoogle = (840.65 / 824.17 - 1);
            var totalChange = changeInApple + changeInGoogle;
            Assert.Equal(portfolioOptimiser.Portfolio, new Dictionary<string, double>() { { "AAPL", changeInApple / totalChange }, { "GOOGL", changeInGoogle / totalChange } });
        }

        [Fact]
        public void Optimise_MultipleHistoricValuesGoingUp_LongStock()
        {
            var stocks = new List<Stock>()
            {
                { new Stock("GOOGL", new List<HistoricPrice>()
                        { new HistoricPrice(new DateTime(2017,3,1), 824.17), new HistoricPrice(new DateTime(2017,3,2), 143.17) },
                  new ForecastPrice(840.65)) },
            };
            PortfolioOptimiser portfolioOptimiser = new PortfolioOptimiser(stocks);

            portfolioOptimiser.Optimise();

            Assert.Equal(new Dictionary<string, double>() { { "GOOGL", 1 } }, portfolioOptimiser.Portfolio);
        }

        [Fact]
        public void Get_Profile_BeforeOptimise_ShouldThrowInvalidOperationException()
        {
            Stock stock = new Stock("GOOGL", new List<HistoricPrice>() { new HistoricPrice(new DateTime(2017,3,1), 824.17) }, new ForecastPrice(840.65));
            PortfolioOptimiser portfolioOptimiser = new PortfolioOptimiser(new List<Stock>() { stock });

            var exception = Assert.Throws<InvalidOperationException>(() => portfolioOptimiser.Portfolio);
            Assert.Equal("Can't retrieve portfolio before optimising", exception.Message);
        }
    }
}
