using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExercisesUnitTests
{
    public enum PriceLevel
    {
        Cheap,
        Medium,
        Expensive
    }
    public class PriceComparer : IEqualityComparer<decimal>
    {
        public bool Equals(decimal x, decimal y)
        {
            return (Level(x) == Level(y));
        }

        public int GetHashCode(decimal i)
        {
            if (Level(i) == PriceLevel.Cheap) return PriceLevel.Cheap.GetHashCode();
            if (Level(i) == PriceLevel.Medium) return PriceLevel.Medium.GetHashCode();
            return PriceLevel.Expensive.GetHashCode();
        }

        public PriceLevel Level(decimal id)
        {
            if (id <= 15M) return PriceLevel.Cheap;
            if (id > 15M && id < 30M) return PriceLevel.Medium;
            return PriceLevel.Expensive;
        }
    }
}
