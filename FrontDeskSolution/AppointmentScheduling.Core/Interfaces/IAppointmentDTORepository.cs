using AppointmentScheduling.Core.Model;
using AppointmentScheduling.Core.Model.ScheduleAggregate;
using System;
using System.Linq;

namespace AppointmentScheduling.Core.Interfaces
{
    public interface IAppointmentDTORepository
    {
        AppointmentDTO GetFromAppointment(Appointment appointment);
    }
}
