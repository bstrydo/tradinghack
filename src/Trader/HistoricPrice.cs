using System;

namespace Trader
{
    public class HistoricPrice
    {
        public double Price { get; private set;}
        public DateTime Date { get; private set; }

        public HistoricPrice(DateTime date, double price)
        {
            Date = date;
            Price = price;
        }
    }
}
