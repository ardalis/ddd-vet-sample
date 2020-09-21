using MediatR;
using StructureMap;
using System;
using System.Collections.Generic;

namespace FrontDesk.SharedKernel
{    

    public abstract class BaseDomainEvent : INotification
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;

        [ThreadStatic]
        private static List<Delegate> actions;

        static BaseDomainEvent()
        {
            Container = ObjectFactory.Container;
        }

        public static IContainer Container { get; set; }
        public static void Register<T>(Action<T> callback) where T : BaseDomainEvent
        {
            if (actions == null)
            {
                actions = new List<Delegate>();
            }
            actions.Add(callback);
        }

        public static void ClearCallbacks()
        {
            actions = null;
        }

        public static void Raise<T>(T args) where T : BaseDomainEvent
        {
            foreach (var handler in Container.GetAllInstances<INotificationHandler<T>>())
            {
                handler.Handle(args, new System.Threading.CancellationToken());
            }

            if (actions != null)
            {
                foreach (var action in actions)
                {
                    if (action is Action<T>)
                    {
                        ((Action<T>)action)(args);
                    }
                }
            }
        }
    }
}