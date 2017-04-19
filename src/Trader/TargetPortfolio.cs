using System.Collections.Generic;

namespace Trader
{
    public class TargetPortfolio : PortfolioItem
    {
        public List<TargetPortfolioItem> PortfolioItems { get; private set; }

        public TargetPortfolio(List<Stock> stocks, Dictionary<string, double> weights)
        {
            PortfolioItems = new List<TargetPortfolioItem>();

            foreach (var stock in stocks)
            {
                PortfolioItems.Add(new TargetPortfolioItem(stock, weights[stock.Symbol]));
            }
        }
    }
}
