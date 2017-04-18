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
            this.currentPortfolio = new CurrentPortfolio(currentStockQuantities, stocks);
            this.targetPortfolio = new TargetPortfolio(stocks, targetPortfolioWeight);
        }

        public void PlaceOrders()
        {
            foreach (PortfolioItem portfolioItem in targetPortfolio.PortfolioItems)
            {
                if (portfolioItem.Weight < 0)
                {
                    exchange.Sell(portfolioItem.Stock.Symbol, currentPortfolio.Quantity(portfolioItem.Stock.Symbol), currentStockPrices[portfolioItem.Stock.Symbol]);
                }
                else
                {
                    double shareDifference = currentPortfolio.Total > 0 ? Math.Round(portfolioItem.Weight - currentPortfolio.Value(portfolioItem.Stock.Symbol) / currentPortfolio.Total, 2) : portfolioItem.Weight;
                    int noOfSharesToTransact = (int) Math.Floor(availableCapital * Math.Abs(shareDifference) / currentStockPrices[portfolioItem.Stock.Symbol]);
                    if (shareDifference > 0)
                    {
                        exchange.Buy(portfolioItem.Stock.Symbol, noOfSharesToTransact, currentStockPrices[portfolioItem.Stock.Symbol]);
                    }
                    else
                    {
                        exchange.Sell(portfolioItem.Stock.Symbol, noOfSharesToTransact, currentStockPrices[portfolioItem.Stock.Symbol]);
                    }
                }
            }
        }
    }
}
