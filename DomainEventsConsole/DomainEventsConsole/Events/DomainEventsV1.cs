using DomainEventsConsole.Interfaces;
using StructureMap;

namespace DomainEventsConsole.Events
{
    public static class DomainEventsV1
    {
        static DomainEventsV1()
        {
            Container = StructureMap.ObjectFactory.Container;
        }

        public static IContainer Container { get; set; }
        public static void Raise<T>(T args) where T : IDomainEvent
        {
            foreach (var handler in Container.GetAllInstances<IHandle<T>>())
            {
                handler.Handle(args);
            }
        }
    }
}