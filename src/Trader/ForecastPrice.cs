namespace Trader
{
   public class ForecastPrice
   {
       public string Symbol { get; private set;}
       public double Price { get; private set;}

       public ForecastPrice(string symbol, double price)
       {
           Symbol = symbol;
           Price = price;
       }
   }
}
