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
                                 select new { Stock = t.Stock, LiveQuantity = l.Quantity, SharePrice = l.SharePrice(), LiveValue = l.Value(), TargetWeight = t.Weight })
            {
                PlaceOrder(item.Stock, item.LiveQuantity, item.SharePrice, item.LiveValue, item.TargetWeight);
            }
        }

        private void PlaceOrder(Stock stock, int liveQuantity, double sharePrice, double liveValue, double targetWeight)
        {
            if (targetWeight < 0)
            {
                exchange.Sell(stock, liveQuantity, sharePrice);
            }
            else
            {
                double portfolioWeightDifference = liveValue > 0 ? Math.Round(targetWeight - liveValue / livePortfolio.Value(), 2) : targetWeight;
                int sharesToTransact = (int)Math.Floor(availableCapital * Math.Abs(portfolioWeightDifference) / sharePrice);
                if (portfolioWeightDifference > 0)
                    exchange.Buy(stock, sharesToTransact, sharePrice);
                else
                    exchange.Sell(stock, sharesToTransact, sharePrice);
            }
        }
    }
}
