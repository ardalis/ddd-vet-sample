using Ardalis.Specification;
using FrontDesk.Core.Aggregates;
using System;
using System.Linq;

namespace FrontDesk.Core.Specifications
{
    public class ScheduleForDateAndClinicSpecification : Specification<Schedule>
    {
        public ScheduleForDateAndClinicSpecification(int clinicId, DateTime date)
        {
            Query
                .Include(nameof(Schedule.Appointments))
                .Where(schedule =>
                    schedule.ClinicId == clinicId &&
                    schedule.Appointments != null &&
                    schedule.Appointments.Any(appointment => ((DateTime?)appointment.TimeRange.Start).Value.Date == ((DateTime?)date).Value.Date));
                        
        }
    }
}
