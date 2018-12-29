using System.Threading.Tasks;

namespace Codez.Uniques
{
    public class NoUniqueness : IUniqueness
    {
        public ValueTask<bool> IsUniqueAsync(string value)
        {
            return new ValueTask<bool>(true);
        }
    }
}