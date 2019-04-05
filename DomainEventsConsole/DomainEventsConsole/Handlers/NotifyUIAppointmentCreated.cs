using DomainEventsConsole.Events;
using DomainEventsConsole.Interfaces;

namespace DomainEventsConsole.Handlers
{
    public class NotifyUIAppointmentCreated : IHandle<AppointmentCreated>
    {
        public void Handle(AppointmentCreated args)
        {
            ConsoleWriter.FromUIEventHandlers("[UI] User Interface informed appointment created for {0}", args.Appointment.EmailAddress);
        }
    }
}