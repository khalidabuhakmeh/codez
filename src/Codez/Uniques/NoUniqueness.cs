using System.Threading.Tasks;

namespace Codez.Uniques
{
    public class NoUniqueness : IUniqueness
    {
        public async ValueTask<bool> IsUniqueAsync(string value)
        {
            return true;
        }
    }
}