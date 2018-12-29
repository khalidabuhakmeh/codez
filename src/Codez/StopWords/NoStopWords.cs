using System.Threading.Tasks;

namespace Codez.StopWords
{
    public class NoStopWords : IStopWords
    {
        public ValueTask<bool> IsAllowedAsync(string value)
        {
            return new ValueTask<bool>(true);
        }
    }
}