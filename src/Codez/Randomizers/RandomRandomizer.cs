using System;
using System.Threading.Tasks;

namespace Codez.Randomizers
{
    public class RandomRandomizer : IRandomizer
    {
        private readonly Random random;

        public RandomRandomizer()
            : this((int)DateTime.UtcNow.Ticks)
        {}

        public RandomRandomizer(int seed)
        {
            this.random = new Random(seed);
        }

        public async ValueTask<int> NextAsync(int size)
        {
            return random.Next(0, size - 1);
        }
    }
}