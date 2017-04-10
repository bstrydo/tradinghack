using System;
using System.Collections.Generic;

namespace Common
{
    public class NoviceTrader
    {
        private Exchange exchange;
        private Dictionary<string, double> currentPortfolio;
        private Dictionary<string, double> targetPortfolio;
        private Dictionary<string, double> currentStockPrices;
        private double availableCapital;

        public NoviceTrader(Exchange exchange, Dictionary <string, double> currentPortfolio, Dictionary<string, double> targetPortfolio, Dictionary<string, double> currentStockPrices, double availableCapital)
        {
            this.exchange = exchange;
            this.currentPortfolio = currentPortfolio;
            this.targetPortfolio = targetPortfolio;
            this.currentStockPrices = currentStockPrices;
            this.availableCapital = availableCapital;
        }

        public void PlaceOrders()
        {
            foreach (string symbol in targetPortfolio.Keys)
            {
                if (currentPortfolio[symbol] <targetPortfolio[symbol])
                {
                    exchange.Buy(symbol, (int)Math.Floor(availableCapital * (targetPortfolio[symbol] -currentPortfolio[symbol])/ currentStockPrices[symbol]));
                }
                else
                {
                    exchange.Sell(symbol, (int)Math.Floor(availableCapital * (currentPortfolio[symbol] -targetPortfolio[symbol])/ currentStockPrices[symbol]));
                }
            }
        }
    }
}
