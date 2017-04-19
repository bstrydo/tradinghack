using System.Linq;
using System.Collections.Generic;

namespace Trader
{
    public class LivePortfolio
    {
        public  List<LivePortfolioItem> PortfolioItems { get; private set; }

        public LivePortfolio(Dictionary<string, int> quantity, List<Stock> stocks)
        {
            this.PortfolioItems = new List<LivePortfolioItem>();
            foreach (var stock in stocks)
            {
                PortfolioItems.Add(new LivePortfolioItem(stock, quantity[stock.Symbol]));
            }
        }

        public double Value { get { return PortfolioItems.Sum(s => s.Value()); } }
    }
}
