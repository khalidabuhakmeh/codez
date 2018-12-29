using System;

using Codez;
using Codez.Alphabets;
using Codez.Generators;
using Codez.Randomizers;
using Codez.StopWords;
using Codez.Transformers;
using Codez.Uniques;

namespace Tests
{
    public class CodeGeneratorTestContext
    {
        public ICodeGenerator CreateGenerator<T>(CodeGeneratorOptions options = null,
                                                 IAlphabet alphabet = null,
                                                 IRandomizer randomizer = null,
                                                 IUniqueness uniqueness = null,
                                                 IStopWords stopWords = null,
                                                 ITransformer transformer = null)
            where T : ICodeGenerator
        {
            return (ICodeGenerator)Activator.CreateInstance(typeof(T), options, alphabet, randomizer, uniqueness, stopWords, transformer);
        }
    }
}
