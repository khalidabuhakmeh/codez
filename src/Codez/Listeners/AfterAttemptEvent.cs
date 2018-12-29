using Codez.Generators;

namespace Codez.Listeners
{
    public class AfterAttemptEvent
    {
        public AfterAttemptEvent(CodeGeneratorResult result)
        {
            Result = new CodeGeneratorResult
            {
                Reason = result.Reason,
                Retries = result.Retries,
                Value = result.Value,
                Success = result.Success
            };
        }

        public CodeGeneratorResult Result { get; }
    }
}