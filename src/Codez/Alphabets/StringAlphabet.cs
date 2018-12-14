using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Codez.Alphabets
{
    public class StringAlphabet : IAlphabet
    {
        public StringAlphabet(string alphabet)
        {
            if (alphabet == null) throw new ArgumentNullException(nameof(alphabet));
            if (alphabet.Length == 0) throw new ArgumentException("empty alphabets are not allowed",nameof(alphabet));
            Characters = new ReadOnlyCollection<char>(alphabet.ToCharArray());
        }

        public IReadOnlyList<char> Characters { get; private set; }        
    }
}