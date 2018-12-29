namespace Codez.Generators
{
    public class CodeGeneratorOptions
    {
        public CodeGeneratorOptions()
        {
            RetryLimit = 5;
        }
        
        public int RetryLimit { get; set; }
    }
}