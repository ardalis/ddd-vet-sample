using DomainEventsConsole.Interfaces;
using DomainEventsConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEventsConsole.Services
{
    public class AppointmentSchedulingService
    {
        private IRepository<Appointment> _apptRepository;
        public AppointmentSchedulingService(IRepository<Appointment> apptRepository)
        {
            _apptRepository = apptRepository;
        }

        public void ScheduleAppointment(string email, DateTime appointmentTime)
        {
            var appointment = Appointment.Create(email);
            _apptRepository.Save(appointment);
        }
    }
}
