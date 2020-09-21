using Ardalis.Specification;
using FrontDesk.Core.Aggregates;
using System;

namespace FrontDesk.Core.Specifications
{
    public class AppointmentByScheduleIdSpecification : Specification<Appointment>
    {
        public AppointmentByScheduleIdSpecification(Guid scheduleId)
        {
            Query.Include(nameof(Appointment.AppointmentType));
            Query.Include(nameof(Appointment.Patient));

            Query
                .Where(appointment => appointment.ScheduleId == scheduleId)
                .OrderBy(appointment => appointment.Title);
        }
    }
}
