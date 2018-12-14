using System.Threading.Tasks;

namespace Codez.Uniques
{
    public interface IUniqueness
    {
        /// <summary>
        /// Determine whether the value is unique.
        /// Uniqueness should be determined by the context and may hit an external resource to do so
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        ValueTask<bool> IsUniqueAsync(string value);
    }
}