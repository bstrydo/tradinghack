using System;

namespace Common
{
    public class Tick
    {
        DateTime TimeStamp {get; }
        double Price {get;}

        public Tick(DateTime timeStamp, double price)
        {
            this.TimeStamp = timeStamp;
            this.Price = price;
        }
    }
}
