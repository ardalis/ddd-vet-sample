using DomainEventsConsole.Events;
using DomainEventsConsole.Interfaces;

namespace DomainEventsConsole.Handlers
{
    public class NotifyUIAppointmentConfirmed : IHandle<AppointmentConfirmed>
    {
        public void Handle(AppointmentConfirmed args)
        {
            ConsoleWriter.FromUIEventHandlers("[UI] User Interface informed appointment for {0} confirmed at {1}", 
                args.Appointment.EmailAddress,
                args.Appointment.ConfirmationReceivedDate.ToString());
        }
    }
}