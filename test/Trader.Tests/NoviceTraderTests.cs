using System.Collections.Generic;
using System;
using Xunit;
using Moq;

namespace Trader.Tests
{
    public class NoviceTraderTests
    {
        private Mock<Exchange> mockExchange = new Mock<Exchange>();
        private double availableCapital = 1000000.00;

        [Theory]
        [InlineData("AAPL", 143.66, 6960)]
        [InlineData("GOOGL", 845.10, 1183)]
        public void PlaceOrders_CurrentPortfolioWithSingleLongPosition_PlaceBuyOrder(string symbol, double price, int expectedNumberOfShares)
        {
            var currentStockQuantities = new Dictionary<string, int>() { { symbol, 0 } };
            var portfolio = new Dictionary<string, double>() { { symbol, 1 } };
            var currentStockPrices = new Dictionary<string, double>() { { symbol, price } };
            NoviceTrader noviceTrader = new NoviceTrader(mockExchange.Object, currentStockQuantities, portfolio, currentStockPrices, availableCapital);

            noviceTrader.PlaceOrders();

            mockExchange.Verify(e => e.Buy(symbol, expectedNumberOfShares, price));
        }

        [Fact]
        public void PlaceOrders_MultipleLongPositions_PlaceWeightedBuyOrders()
        {
            var currentStockQuantities = new Dictionary<string, int>() { { "GOOGL", 0 }, { "AAPL", 0 } };
            var portfolio = new Dictionary<string, double>() { { "GOOGL", 0.75 }, { "AAPL", 0.25 } };
            var currentStockPrices = new Dictionary<string, double>() { { "GOOGL", 845.10 }, { "AAPL", 143.66 } };
            NoviceTrader noviceTrader = new NoviceTrader(mockExchange.Object, currentStockQuantities, portfolio, currentStockPrices, availableCapital);
            var actualBuyOrders = new List<Tuple<string, int, double>>();
            mockExchange
                .Setup(e => e.Buy(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<double>()))
                .Callback<string, int, double>((symbol, numberOfShares, price) => actualBuyOrders.Add(new Tuple<string, int, double>(symbol, numberOfShares, price)));

            noviceTrader.PlaceOrders();

            Assert.Equal(actualBuyOrders[0], new Tuple<string, int, double>("GOOGL", 887, 845.10));
            Assert.Equal(actualBuyOrders[1], new Tuple<string, int, double>("AAPL", 1740, 143.66));
        }

        [Fact]
        public void PlaceOrders_CurrentPortfolioWeightsAboveZero_BuyAndSellToMatch()
        {
            var currentStockQuantities = new Dictionary<string, int>() { { "GOOGL", 887 }, { "AAPL", 1740 } };
            var targetPortfolio = new Dictionary<string, double>() { { "GOOGL", 0.50 }, { "AAPL", 0.50 } };
            var currentStockPrices = new Dictionary<string, double>() { { "GOOGL", 845.10 }, { "AAPL", 143.66 } };
            NoviceTrader noviceTrader = new NoviceTrader(mockExchange.Object, currentStockQuantities, targetPortfolio, currentStockPrices, availableCapital);

            noviceTrader.PlaceOrders();

            mockExchange.Verify(e => e.Sell("GOOGL", 295, 845.10));
            mockExchange.Verify(e => e.Buy("AAPL", 1740, 143.66));
        }

        [Fact]
        public void PlaceOrders_CurrentPortfolioSomeWeightsBelowZero_BuyAndSellToMatch()
        {
            var currentStockQuantities = new Dictionary<string, int>() { { "GOOGL", 887 }, { "AAPL", 1740 } };
            var targetPortfolio = new Dictionary<string, double>() { { "GOOGL", 0.50 }, { "AAPL", -0.50 } };
            var currentStockPrices = new Dictionary<string, double>() { { "GOOGL", 845.10 }, { "AAPL", 143.66 } };
            NoviceTrader noviceTrader = new NoviceTrader(mockExchange.Object, currentStockQuantities, targetPortfolio, currentStockPrices, availableCapital);

            noviceTrader.PlaceOrders();

            mockExchange.Verify(e => e.Sell("GOOGL", 295, 845.10));
            mockExchange.Verify(e => e.Sell("AAPL", 1740, 143.66));
        }
    }
}
