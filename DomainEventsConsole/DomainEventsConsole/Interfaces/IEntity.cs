using System;

namespace DomainEventsConsole.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}