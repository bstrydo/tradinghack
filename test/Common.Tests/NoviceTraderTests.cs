using System.Collections.Generic;
using System;
using Xunit;
using Moq;

namespace Common.Tests
{
    public class NoviceTraderTests
    {
		private Mock<Exchange> mockExchange = new Mock<Exchange>();
		private double availableCapital = 1000000.00;

		[Fact]
		public void CanPlaceASellOrder()
		{
			var portfolio = new Dictionary<string, double>() { {"Apple", 92.61} };
			var currentStockPrices = new Dictionary<string, double>() { { "Apple", 143.66 } };
			NoviceTrader noviceTrader = new NoviceTrader(mockExchange.Object, portfolio, currentStockPrices, availableCapital);
			noviceTrader.Sell();
			mockExchange.Verify(e => e.Sell());
		}

		[Theory]
		[InlineData("AAPL", 143.66, 6960)]
		[InlineData("GOOGL", 845.10, 1183)]
		public void GivenLongPositionShouldBuy(string symbol, double price, int expectedNumberOfShares)
		{
			var portfolio = new Dictionary<string, double>() { {symbol, 1} };
			var currentStockPrices = new Dictionary<string, double>() { { symbol, price } };
			NoviceTrader noviceTrader = new NoviceTrader(mockExchange.Object, portfolio, currentStockPrices, availableCapital);

			noviceTrader.PlaceOrders();

			mockExchange.Verify(e => e.Buy(symbol, expectedNumberOfShares));
		}

		[Fact]
		public void GivenMultipleLongPositionsShouldBuy()
		{
			var portfolio = new Dictionary<string, double>() { {"GOOGL", 0.75}, {"AAPL", 0.25} };
			var currentStockPrices = new Dictionary<string, double>() { {"GOOGL", 845.10}, {"AAPL", 143.66} };
			NoviceTrader noviceTrader = new NoviceTrader(mockExchange.Object, portfolio, currentStockPrices, availableCapital);
			var actualBuyOrders = new List<Tuple<string, int>>();
			mockExchange
				.Setup(e => e.Buy(It.IsAny<string>(), It.IsAny<int>()))
				.Callback<string, int>((symbol, numberOfShares) => actualBuyOrders.Add(new Tuple<string, int>(symbol, numberOfShares)));

			noviceTrader.PlaceOrders();

			Assert.Equal(actualBuyOrders[0], new Tuple<string, int>("GOOGL", 887));
			Assert.Equal(actualBuyOrders[1], new Tuple<string, int>("AAPL", 1740));
		}
    }
}
