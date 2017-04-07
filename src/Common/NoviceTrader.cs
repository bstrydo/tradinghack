using System;
using System.Collections.Generic;

namespace Common
{
    public class NoviceTrader
    {
        private Exchange exchange;
        private Dictionary<string, double> portfolio;
        private Dictionary<string, double> currentStockPrices;
        private double availableCapital;

        public NoviceTrader(Exchange exchange, Dictionary<string, double> portfolio, Dictionary<string, double> currentStockPrices, double availableCapital)
        {
            this.exchange = exchange;
            this.portfolio = portfolio;
            this.currentStockPrices = currentStockPrices;
            this.availableCapital = availableCapital;

        }

        public void Sell()
        {
            exchange.Sell();
        }

        public void PlaceOrders()
        {
            foreach (string symbol in portfolio.Keys)
            {
                Buy(symbol, (int)Math.Floor(availableCapital * portfolio[symbol] / currentStockPrices[symbol]));
            }
        }

        private void Buy(string symbol, int numberOfShares)
        {
            exchange.Buy(symbol, numberOfShares);
        }
    }
}
