namespace Trader
{
    public class PortfolioItem
    {
        public Stock Stock { get; private set; }
        public int Quantity { get; private set; }
        public double Weight { get; private set; }

        public PortfolioItem(Stock stock, int quantity)
        {
            Stock = stock;
            Quantity = quantity;
        }

        public PortfolioItem(Stock stock, double weight)
        {
            Stock = stock;
            Weight = weight;
        }

        public double GetPortfolioValue()
        {
            return Stock.ComputeValue(Quantity);
        }
    }
}
