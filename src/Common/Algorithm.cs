using System;
using System.Collections.Generic;
using System.Linq;
namespace Common
{
    public interface Algorithm
    {
        AlgorithmResult run(List<Tick> ticks);
    }
}
