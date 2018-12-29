using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Codez.Randomizers;

namespace Codez
{
    /// <summary>
    /// Shuffle method using Jon Skeet's implementation of Durstenfeld's Fischer-Yates variant.
    /// https://stackoverflow.com/questions/1287567/is-using-random-and-orderby-a-good-shuffle-algorithm
    /// </summary>
    internal static class EnumerableExtensions
    {
        public static async Task<IEnumerable<T>> ShuffleAsync<T>(this IEnumerable<T> source, IRandomizer randomizer)
        {
            var elements = source.ToArray();
            // Note i > 0 to avoid final pointless iteration
            for (var i = elements.Length - 1; i > 0; i--)
            {
                // Swap element "i" with a random earlier element it (or itself)
                var swapIndex = await randomizer.NextAsync(i + 1);
                var tmp = elements[i];
                elements[i] = elements[swapIndex];
                elements[swapIndex] = tmp;
            }

            return elements;
        }
    }
}
