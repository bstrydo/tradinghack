using System.Collections.Generic;

namespace Trader
{
    public class TargetPortfolio : Portfolio
    {
        public TargetPortfolio(List<Stock> stocks, Dictionary<string, double> weights)
        {
            PortfolioItems = new List<PortfolioItem>();

            foreach (var stock in stocks)
            {
                PortfolioItems.Add(new TargetPortfolioItem(stock, weights[stock.Symbol]));
            }
        }
    }
}
