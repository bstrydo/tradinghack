namespace Trader
{
    public interface Exchange
    {
        void Buy(Stock stock, int numberOfShares, double price);
        void Sell(Stock stock, int numberOfShares, double price);
    }
}
