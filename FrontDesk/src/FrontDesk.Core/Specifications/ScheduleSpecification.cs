using Ardalis.Specification;
using FrontDesk.Core.Aggregates;
using System;
using System.Linq;

namespace FrontDesk.Core.Specifications
{
    public class ScheduleForDateSpecification : Specification<Schedule>
    {
        public ScheduleForDateSpecification(int clinicId, DateTime date)
        {
            Query
                .Include(nameof(Appointment))
                .Where(schedule => schedule.ClinicId == clinicId && schedule.Appointments.Any(appointment => appointment.TimeRange.Start == date));
        }
    }
}
