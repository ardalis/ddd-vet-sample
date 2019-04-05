using AppointmentScheduling.Core.Model.ScheduleAggregate;
using System;
using System.Linq;

namespace AppointmentScheduling.Core.Interfaces
{
    /// <summary>
    /// Note: This repository will save changes with each method
    /// </summary>
    public interface IScheduleRepository
    {
        Schedule GetScheduleForDate(int clinicId, DateTime date);
        void Update(Schedule schedule);
    }
}
