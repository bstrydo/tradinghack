using System.Collections.Generic;

namespace Trader
{
    public class TargetPortfolio
    {
        public List<PortfolioItem> PortfolioItems { get; private set; }
        public TargetPortfolio(List<Stock> stocks, Dictionary<string, double> weights)
        {
            PortfolioItems = new List<PortfolioItem>();

            foreach (var stock in stocks)
            {
                PortfolioItems.Add(new PortfolioItem(stock, weights[stock.Symbol]));
            }
        }
    }
}
