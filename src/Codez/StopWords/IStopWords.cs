using System.Threading.Tasks;

namespace Codez.StopWords
{
    public interface IStopWords
    {
        /// <summary>
        /// Computers can generate the strangest things.
        /// Use this interface to determine if the code generated is appropriate for your #$%@ing audience.  
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        ValueTask<bool> IsAllowedAsync(string value);
    }
}