using System;
using System.Collections.Generic;
using System.Linq;

namespace Trader
{
    public class NoviceTrader
    {
        private Exchange exchange;
        private Dictionary<string, double> targetPortfolioWeight;
        private Dictionary<string, double> currentStockPrices;
        private double availableCapital;
        private LivePortfolio livePortfolio;
        private TargetPortfolio targetPortfolio;

        public NoviceTrader(Exchange exchange, Dictionary<string, int> currentStockQuantities, Dictionary<string, double> targetPortfolioWeight, Dictionary<string, double> currentStockPrices, double availableCapital)
        {
            this.exchange = exchange;
            this.targetPortfolioWeight = targetPortfolioWeight;
            this.currentStockPrices = currentStockPrices;
            this.availableCapital = availableCapital;
            var stocks = new List<Stock>();
            foreach (var symbol in currentStockPrices.Keys)
            {
                stocks.Add(new Stock(symbol, currentStockPrices[symbol]));
            }
            this.livePortfolio = new LivePortfolio(currentStockQuantities, stocks);
            this.targetPortfolio = new TargetPortfolio(stocks, targetPortfolioWeight);
        }

        public void PlaceOrders()
        {
            foreach (TargetPortfolioItem targetPortfolioItem in targetPortfolio.PortfolioItems)
            {
                var livePortfolioItem = livePortfolio.PortfolioItems.Single(i => i.Stock == targetPortfolioItem.Stock);

                if (targetPortfolioItem.Weight < 0)
                {
                    exchange.Sell(livePortfolioItem.Stock, livePortfolioItem.Quantity, livePortfolioItem.SharePrice());
                }
                else
                {
                    double shareDifference = livePortfolio.Value() > 0 ? Math.Round(targetPortfolioItem.Weight - livePortfolioItem.Value() / livePortfolio.Value(), 2) : targetPortfolioItem.Weight;
                    int noOfSharesToTransact = (int) Math.Floor(availableCapital * Math.Abs(shareDifference) / livePortfolioItem.SharePrice());
                    if (shareDifference > 0)
                    {
                        exchange.Buy(livePortfolioItem.Stock, noOfSharesToTransact, livePortfolioItem.SharePrice());
                    }
                    else
                    {
                        exchange.Sell(livePortfolioItem.Stock, noOfSharesToTransact, livePortfolioItem.SharePrice());
                    }
                }
            }
        }
    }
}
