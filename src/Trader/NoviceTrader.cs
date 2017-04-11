using System;
using System.Collections.Generic;

namespace Trader
{
    public class NoviceTrader
    {
        private Exchange exchange;
        private Dictionary<string, double> targetPortfolioWeight;
        private Dictionary<string, double> currentStockPrices;
        private double availableCapital;
        private CurrentPortfolio currentPortfolio;

        public NoviceTrader(Exchange exchange, Dictionary <string, int> currentStockQuantities, Dictionary<string, double> targetPortfolioWeight, Dictionary<string, double> currentStockPrices, double availableCapital)
        {
            this.exchange = exchange;
            this.targetPortfolioWeight = targetPortfolioWeight;
            this.currentStockPrices = currentStockPrices;
            this.availableCapital = availableCapital;
            this.currentPortfolio = new CurrentPortfolio(currentStockQuantities, currentStockPrices);
        }

        public void PlaceOrders()
        {
            foreach (string symbol in targetPortfolioWeight.Keys)
            {
                PlaceOrder(symbol);
            }
        }

        private void PlaceOrder(string symbol)
        {
            double shareDifference = currentPortfolio.Total > 0 ? Math.Round(targetPortfolioWeight[symbol] - currentPortfolio.Value(symbol) / currentPortfolio.Total, 2) : targetPortfolioWeight[symbol];
            int noOfSharesToTransact = (int) Math.Floor(availableCapital * Math.Abs(shareDifference) / currentStockPrices[symbol]);
            if (shareDifference > 0)
            {
                exchange.Buy(symbol, noOfSharesToTransact, currentStockPrices[symbol]);
            }
            else
            {
                exchange.Sell(symbol, noOfSharesToTransact, currentStockPrices[symbol]);
            }
        }
    }
}
