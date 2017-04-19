using System.Linq;
using System.Collections.Generic;

namespace Trader
{
    public class LivePortfolio : Portfolio
    {
        public LivePortfolio(Dictionary<string, int> quantity, List<Stock> stocks)
        {
            PortfolioItems = new List<PortfolioItem>();
            foreach (var stock in stocks)
            {
                PortfolioItems.Add(new LivePortfolioItem(stock, quantity[stock.Symbol]));
            }
        }

        public double Value()
        {
            return PortfolioItems.Sum(s => (s as LivePortfolioItem).Value());
        }
    }
}
