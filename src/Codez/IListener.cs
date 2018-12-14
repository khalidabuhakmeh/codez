using System.Threading.Tasks;

namespace Codez
{
    public interface IListener
    {
        Task OnBeforeAttempt(BeforeAttemptEvent @event);
        Task OnAfterAttempt(AfterAttemptEvent @event);
    }

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

    public class BeforeAttemptEvent
    {
        public BeforeAttemptEvent(int attempt)
        {
            Attempt = attempt;
        }
        
        public int Attempt { get; }
    }
}