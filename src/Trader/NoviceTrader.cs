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
            foreach (var item in from t in targetPortfolio.PortfolioItems
                                 join l in livePortfolio.PortfolioItems on t.Stock equals l.Stock
                                 select new { t, l })
            {
                PlaceOrder(item.t as TargetPortfolioItem, item.l as LivePortfolioItem);
            }
        }

        private void PlaceOrder(TargetPortfolioItem target, LivePortfolioItem live)
        {
            if (target.Weight < 0)
            {
                exchange.Sell(target.Stock, live.Quantity, live.SharePrice());
            }
            else
            {
                double portfolioWeightDifference = live.Value() > 0 ? Math.Round(target.Weight - live.Value() / livePortfolio.Value(), 2) : target.Weight;
                int sharesToTransact = (int)Math.Floor(availableCapital * Math.Abs(portfolioWeightDifference) / live.SharePrice());
                if (portfolioWeightDifference > 0)
                    exchange.Buy(target.Stock, sharesToTransact, live.SharePrice());
                else
                    exchange.Sell(target.Stock, sharesToTransact, live.SharePrice());
            }
        }
    }
}
