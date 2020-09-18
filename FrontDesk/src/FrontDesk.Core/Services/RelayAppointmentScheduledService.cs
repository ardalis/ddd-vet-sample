using FrontDesk.Core.Events;
using FrontDesk.SharedKernel.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FrontDesk.Core.Services
{
    /// <summary>
    /// Post appointmentscheduledevent to message bus to allow confirmation emails to be sent
    /// </summary>
    public class RelayAppointmentScheduledService : INotificationHandler<AppointmentScheduledEvent>
    {
        private readonly IRepository _apptRepository;

        public RelayAppointmentScheduledService(IRepository apptRepository)
        {
            this._apptRepository = apptRepository;
        }

        public async Task Handle(AppointmentScheduledEvent appointmentScheduledEvent, CancellationToken cancellationToken)
        {
            //TODO: need to do this
            //AppointmentDTO appointment = _apptRepository.GetFromAppointment(appointmentScheduledEvent.AppointmentScheduled);

            // we are translating from a domain event to an application event here
            //var newEvent = new Model.ApplicationEvents.AppointmentScheduledEvent(appointment);

            //_messagePublisher.Publish(newEvent);
        }
    }
}
