namespace FrontDesk.SharedKernel.Interfaces
{
    public interface IApplicationEvent
    {
        string EventType { get; }
    }
}
