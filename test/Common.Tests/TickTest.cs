using System;
using Xunit;
using Common;


namespace Common.Test
{
    public class TickTest
    {
        [Fact]
        public void GetPriceShouldReturnThePricePassedAtConstruction()
        {
            var tick = new Tick(new DateTime(), 1.92389);
            Assert.Equal(1.92389, tick.price);
        }
    }
}
