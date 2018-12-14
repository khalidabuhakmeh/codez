using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Codez.Alphabets
{
    public class AsciiAlphabet : IAlphabet
    {
        private static readonly ReadOnlyCollection<char> Ascii
            = new ReadOnlyCollection<char>(Enumerable.Range(33, 94).Select(x => (char)x).ToArray());
        
        public AsciiAlphabet()
        {
            Characters = Ascii;
        }
        
        public IReadOnlyList<char> Characters { get; }
        public char Get(int index)
        {
            return Characters[index];
        }

        public int Count => Characters.Count;
    }
}