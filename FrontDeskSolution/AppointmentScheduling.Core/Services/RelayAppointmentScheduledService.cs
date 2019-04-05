using AppointmentScheduling.Core.Interfaces;
using AppointmentScheduling.Core.Model;
using AppointmentScheduling.Core.Model.Events;
using FrontDesk.SharedKernel.Interfaces;
using System;
using System.Linq;

namespace AppointmentScheduling.Core.Services
{
    /// <summary>
    /// Post appointmentscheduledevent to message bus to allow confirmation emails to be sent
    /// </summary>
    public class RelayAppointmentScheduledService : IHandle<AppointmentScheduledEvent>
    {
        private readonly IAppointmentDTORepository _apptRepository;

        private readonly IMessagePublisher _messagePublisher;

        public RelayAppointmentScheduledService(IAppointmentDTORepository apptRepository, IMessagePublisher messagePublisher)
        {
            this._apptRepository = apptRepository;
            this._messagePublisher = messagePublisher;
        }

        public void Handle(AppointmentScheduledEvent appointmentScheduledEvent)
        {
            AppointmentDTO appointment = _apptRepository.GetFromAppointment(appointmentScheduledEvent.AppointmentScheduled);

            // we are translating from a domain event to an application event here
            var newEvent = new AppointmentScheduling.Core.Model.ApplicationEvents.AppointmentScheduledEvent(appointment);

            _messagePublisher.Publish(newEvent);
        }
    }




    ///// <summary>
    ///// Sample for a Slide
    ///// </summary>
    //public class RelayAppointmentScheduledService : IHandle<AppointmentScheduledEvent>
    //{
    //    public void Handle(AppointmentScheduledEvent domainEvent)
    //    {
    //        dynamic newEvent = new
    //        {
    //            Start = domainEvent.AppointmentScheduled.TimeRange.Start,
    //            End = domainEvent.AppointmentScheduled.TimeRange.End,
    //            Client = "",
    //            Patient = "",
    //            AppointmentType = "",
    //            Doctor = ""
    //        };

    //        #region Fetch the Client name, Patient name, Appointment Type, and Doctor information
    //        #endregion

    //        #region Publish the newEvent to a message bus for other systems to consume
    //        newEvent.ToString(); // added just to remove green squiggles in above code
    //        #endregion
    //    }
    //}
}
