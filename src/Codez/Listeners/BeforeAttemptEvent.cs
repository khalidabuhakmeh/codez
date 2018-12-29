namespace Codez.Listeners
{
    public class BeforeAttemptEvent
    {
        public BeforeAttemptEvent(int attempt)
        {
            Attempt = attempt;
        }
        
        public int Attempt { get; }
    }
}