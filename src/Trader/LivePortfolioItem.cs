namespace Trader
{
    public class LivePortfolioItem : PortfolioItem
    {
        public int Quantity { get; private set; }

        public LivePortfolioItem(Stock stock, int quantity)
        {
            Stock = stock;
            Quantity = quantity;
        }

        public double Value()
        {
            return Stock.CurrentPrice() * Quantity;
        }

        public double SharePrice()
        {
            return Stock.CurrentPrice();
        }
    }
}
