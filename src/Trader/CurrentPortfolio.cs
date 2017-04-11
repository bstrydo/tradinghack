using System.Linq;
using System.Collections.Generic;

namespace Trader
{
    public class CurrentPortfolio
    {
        private Dictionary<string, double> stockValue;
        private double total;

        public CurrentPortfolio(Dictionary<string, int> quantity, Dictionary<string, double> price)
        {
            this.stockValue = new Dictionary<string, double>();
            foreach (var symbol in quantity.Keys)
            {
                stockValue[symbol] = quantity[symbol] * price[symbol];
            }
            total = stockValue.Keys.Sum(s => Value(s));
        }

        public double Value(string symbol)
        {
            return stockValue[symbol];
        }

        public double Total { get { return total; } }
    }
}
