namespace Codez.Generators
{
    public class CodeGeneratorResult
    {
        public bool Success { get; set; }
        public string Value { get; set; }
        
        public FailureReasonType Reason { get; set; }

        public int Retries { get; set; } = 0;
    }
}