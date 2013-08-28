namespace FrontDesk.Core.Interfaces
{
    public interface IMessageBus<T> where T : IEvent
    {
        void Publish(T message);
    }
}