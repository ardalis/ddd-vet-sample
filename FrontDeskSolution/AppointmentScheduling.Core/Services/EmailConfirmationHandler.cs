using AppointmentScheduling.Core.Interfaces;
using AppointmentScheduling.Core.Model.ApplicationEvents;
using FrontDesk.SharedKernel.Interfaces;
using System;
using System.Linq;

namespace AppointmentScheduling.Core.Services
{
    public class EmailConfirmationHandler : IHandle<AppointmentConfirmedEvent>
    {
        private readonly IScheduleRepository _scheduleRepository;

        private readonly IApplicationSettings _settings;

        public EmailConfirmationHandler(IScheduleRepository scheduleRepository, IApplicationSettings settings)
        {
            this._scheduleRepository = scheduleRepository;
            this._settings = settings;
        }

        public void Handle(AppointmentConfirmedEvent appointmentConfirmedEvent)
        {
            // Note: In this demo this only works for appointments scheduled on TestDate
            var schedule = _scheduleRepository.GetScheduleForDate(_settings.ClinicId, _settings.TestDate);

            var appointmentToConfirm = schedule.Appointments.FirstOrDefault(a => a.Id == appointmentConfirmedEvent.AppointmentId);

            appointmentToConfirm.Confirm(appointmentConfirmedEvent.DateTimeEventOccurred);

            _scheduleRepository.Update(schedule);
        }
    }
}
