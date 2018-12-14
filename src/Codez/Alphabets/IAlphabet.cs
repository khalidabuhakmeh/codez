using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codez.Alphabets
{
    public interface IAlphabet
    {
        char Get(int index);
        int Count { get; }
    }
}