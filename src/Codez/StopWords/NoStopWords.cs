using System.Threading.Tasks;

namespace Codez.StopWords
{
    public class NoStopWords : IStopWords
    {
        public async ValueTask<bool> IsAllowedAsync(string value)
        {
            return true;
        }
    }
}