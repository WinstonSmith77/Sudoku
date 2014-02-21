using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public static class TestHelpers
    {
        private const int _defaultSeed = 6589;

        public static List<T> Shuffle<T>(this IList<T> input, int seed = _defaultSeed)
        {
            var randomOrder = CreateRandomRangeFromZero(input.Count, seed);

            var output = randomOrder.Aggregate(new List<T>(), (result, item) =>
                {
                    result.Add(input[item]);
                    return result;
                });

            return output;
        }

        public static IEnumerable<int> CreateRandomRangeFromZero(int length, int seed = _defaultSeed)
        {
            var random = new Random(seed);
            var order = Enumerable.Range(0, length).ToList();

            var shuffledOrder = new List<int>();

            for (; order.Count != 0; )
            {
                var randomIndex = random.Next(order.Count);
                shuffledOrder.Add(order[randomIndex]);
                order.RemoveAt(randomIndex);
            }

            return shuffledOrder;
        }
    }
}