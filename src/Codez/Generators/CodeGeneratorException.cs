using System;

namespace Codez.Generators
{
    public class CodeGeneratorException : Exception
    {
        public CodeGeneratorResult Result { get; }

        public CodeGeneratorException(CodeGeneratorResult result)
            :base(BuildMessage(result))
        {
            Result = result;

        }

        private static string BuildMessage(CodeGeneratorResult result)
        {
            switch (result.Reason)
            {                
                case FailureReasonType.Uniqueness:
                    return $"Failed due to uniqueness after {result.Retries} attempts.";
                case FailureReasonType.Stopped:
                    return $"Failed due to stop words after {result.Retries} attempts.";
                case FailureReasonType.None:
                default:
                    return "Unknown exception during code generation";
            }
        }
    }
}