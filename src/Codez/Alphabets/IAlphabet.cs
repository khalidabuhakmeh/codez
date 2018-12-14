using System.Collections.Generic;

namespace Codez.Alphabets
{
    public interface IAlphabet
    {
        IReadOnlyList<char> Characters { get; }        
    }
}