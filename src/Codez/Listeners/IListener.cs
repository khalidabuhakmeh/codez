using System.Threading.Tasks;

namespace Codez.Listeners
{
    public interface IListener
    {
        Task OnBeforeAttempt(BeforeAttemptEvent @event);
        Task OnAfterAttempt(AfterAttemptEvent @event);
    }
}