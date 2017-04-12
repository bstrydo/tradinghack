namespace Trader
{
    public class HistoricPrice
    {
        public string Symbol { get; private set;}
        public double Price { get; private set;}

        public HistoricPrice(string symbol, double price)
        {
            Symbol = symbol;
            Price = price;
        }
    }
}
