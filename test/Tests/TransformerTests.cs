using System.Collections.Generic;
using System.Threading.Tasks;
using Codez;
using Codez.Alphabets;
using Codez.Generators;
using Codez.Transformers;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class TransformerTests
    {
        private readonly ITestOutputHelper output;

        public TransformerTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task Can_generate_and_transform_to_container_names()
        {
            var generator = new DefaultCodeGenerator(
                alphabet: new StringAlphabet("0123456789"),
                transformer: new ContainerNamesTransformer()
            );

            var result = await generator.GenerateAsync(2);

            output.WriteLine(result);

            Assert.NotNull(result);
            Assert.Contains("_", result);
        }

        [Fact]
        public async Task Can_fail_to_transform()
        {
            var generator = new DefaultCodeGenerator(
                alphabet: new StringAlphabet("0123456789"),
                transformer: new ContainerNamesTransformer()
            );

            var result = await generator.TryGenerateAsync(1);

            Assert.False(result.Success);
            Assert.Equal(FailureReasonType.Transform, result.Reason);
        }

        public class ContainerNamesTransformer : ITransformer
        {
            /// <summary>
            /// Space dudes
            /// </summary>
            private readonly Dictionary<char, string> names = new Dictionary<char, string>
            {
                {'0', "Surfer"},
                {'1', "Armstrong" },
                {'2', "Aldrin"},
                {'3', "Bowie"},
                {'4', "Baker"},
                {'5', "Starlord"},
                {'6', "Conrad"},
                {'7', "Duke"},
                {'8', "Doctor"},
                {'9', "Galactus" }
            };

            private readonly Dictionary<char, string> adjectives = new Dictionary<char, string>
            {
                { '0', "Funny"},
                { '1', "Sleepy"},
                { '2', "Happy"},
                { '3', "Boring"},
                { '4', "Manic"},
                { '5', "Excited"},
                { '6', "Gassy" },
                { '7', "Cuddly" },
                { '8', "Smelly" },
                { '9', "Scratchy" }
            };

            public ValueTask<CodeGeneratorResult> Transform(CodeGeneratorResult result)
            {
                if (result.Value.Length != 2)
                    return new ValueTask<CodeGeneratorResult>(new CodeGeneratorResult
                    {
                        Value = null,
                        Reason = FailureReasonType.Transform,
                        Retries = result.Retries,
                        Success = false
                    });

                var adjective = adjectives[result.Value[0]];
                var name = names[result.Value[1]];

                return new ValueTask<CodeGeneratorResult>(new CodeGeneratorResult
                {
                    Value = $"{adjective}_{name}",
                    Reason = result.Reason,
                    Retries = result.Retries,
                    Success = result.Success
                });
            }
        }
    }
}