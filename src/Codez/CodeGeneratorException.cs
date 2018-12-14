using System;

namespace Codez
{
    public class CodeGeneratorException : Exception
    {
        public CodeGeneratorException(string message)
            :base(message)
        {
        }
    }
}