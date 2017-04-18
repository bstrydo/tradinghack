using System.Linq;
using System.Collections.Generic;

namespace Trader
{
    public class CurrentPortfolio
    {
        private List<PortfolioItem> portfolioItems;

        public CurrentPortfolio(Dictionary<string, int> quantity, List<Stock> stocks)
        {
            this.portfolioItems = new List<PortfolioItem>();
            foreach (var stock in stocks)
            {
                portfolioItems.Add(new PortfolioItem(stock, quantity[stock.Symbol]));
            }
        }

        public double Value(string symbol)
        {
            return portfolioItems.Single(i => i.Stock.Symbol == symbol).GetPortfolioValue();
        }

        public int Quantity(string symbol)
        {
            return portfolioItems.Single(i => i.Stock.Symbol == symbol).Quantity;
        }

        public double Total { get { return portfolioItems.Sum(s => s.GetPortfolioValue()); } }
    }
}
