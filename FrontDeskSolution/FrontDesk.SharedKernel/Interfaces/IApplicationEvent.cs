namespace FrontDesk.SharedKernel.Interfaces
{
    public interface IApplicationEvent : IDomainEvent
    {
        string EventType { get; }
    }
}