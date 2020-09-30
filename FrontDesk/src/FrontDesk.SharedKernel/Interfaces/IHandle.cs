using System.Threading.Tasks;

namespace FrontDesk.SharedKernel.Interfaces
{
    public interface IHandle<T> where T : BaseDomainEvent
    {
        Task HandleAsync(T args);
    }
}
