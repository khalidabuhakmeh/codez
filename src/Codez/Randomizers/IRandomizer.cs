using System.Threading.Tasks;

namespace Codez.Randomizers
{
    public interface IRandomizer
    {
        /// <summary>
        /// Should return a value between 0 and the size specified
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        ValueTask<int> NextAsync(int size);
    }
}