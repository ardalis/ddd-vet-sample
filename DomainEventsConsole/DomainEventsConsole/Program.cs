using DomainEventsConsole.Handlers;
using DomainEventsConsole.Interfaces;
using DomainEventsConsole.Repositories;
using DomainEventsConsole.Model;
using StructureMap;
using System;
using System.Linq;
using DomainEventsConsole.Services;

namespace DomainEventsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            InitIoC();
            Console.WriteLine("Starting application.");

            var service = ObjectFactory.GetInstance<AppointmentSchedulingService>();

            Console.WriteLine("Scheduling an appointment.");
            service.ScheduleAppointment("steve@test1.com", DateTime.Now);

            Console.WriteLine("Creating an appointment.");
            var appointment = Appointment.Create("steve@test2.com");
            Console.WriteLine("Confirming an appointment.");
            appointment.Confirm(DateTime.Now);

            Console.WriteLine("Application done.");
            Console.ReadLine();
        }

        private static void InitIoC()
        {
            ObjectFactory.Configure(config =>
            {
                config.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.IncludeNamespaceContainingType<NotifyUserAppointmentCreated>(); // specify where handlers are located
                    scan.ConnectImplementationsToTypesClosing(typeof(IHandle<>));
                    scan.IncludeNamespace("DomainEventsConsole.Repositories");
                    scan.ConnectImplementationsToTypesClosing(typeof(IRepository<>));
                });

                config.For(typeof(IRepository<>)).Use(typeof(Repository<>));
            });
        }
    }
}
