using System;

namespace DomainEventsConsole.Interfaces
{
    public interface IDomainEvent
    {
        DateTime DateOccurred { get; }
    }
}
