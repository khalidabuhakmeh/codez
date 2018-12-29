using System.Text;
using System.Threading.Tasks;
using Codez.Alphabets;
using Codez.Listeners;
using Codez.Randomizers;
using Codez.StopWords;
using Codez.Transformers;
using Codez.Uniques;

namespace Codez.Generators
{
    /// <summary>
    /// Default Code Generator
    /// </summary>
    public class DefaultCodeGenerator : CodeGeneratorBase, ICodeGenerator
    {
        private readonly StringBuilder sb = new StringBuilder();

        public DefaultCodeGenerator(CodeGeneratorOptions options = null,
                                IAlphabet alphabet = null ,
                                IRandomizer randomizer = null,
                                IUniqueness uniqueness = null, 
                                IStopWords stopWords = null,
                                ITransformer transformer = null
                            )
            : base(options, alphabet, randomizer, uniqueness, stopWords, transformer)
        {
        }

        protected override async Task<string> GenerateAttemptAsync(int length)
        {
            for (var i = 0; i < length; i++)
            {
                var characterCount = alphabet.Count;
                var index = await randomizer.NextAsync(characterCount);
                var character = alphabet.Get(index);

                sb.Append(character);
            }

            return sb.ToString();
        }

        protected override async Task OnAfterAttempt(AfterAttemptEvent @event)
        {
            sb.Clear();
            await base.OnAfterAttempt(@event);
        }
    }
}