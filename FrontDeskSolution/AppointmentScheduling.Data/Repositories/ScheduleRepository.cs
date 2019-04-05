using System.Collections.Generic;
using System.Data.Entity;
using AppointmentScheduling.Core.Interfaces;
using System;
using System.Linq;
using AppointmentScheduling.Core.Model.ScheduleAggregate;
using FrontDesk.SharedKernel;
using FrontDesk.SharedKernel.Enums;

namespace AppointmentScheduling.Data.Repositories
{
    public class ScheduleRepository : IScheduleRepository, IDisposable
    {
        private readonly SchedulingContext _context;

        public ScheduleRepository(SchedulingContext context)
        {
            this._context = context;
        }

        public void Update(Schedule schedule)
        {
            // add and delete appointments
            foreach (var appointment in schedule.Appointments)
            {
                if (appointment.State == TrackingState.Added)
                {
                    _context.Entry(appointment).State = EntityState.Added;
                }
                if (appointment.State == TrackingState.Modified)
                {
                    _context.Entry(appointment).State = EntityState.Modified;
                }
                if (appointment.State == TrackingState.Deleted)
                {
                    _context.Entry(appointment).State = EntityState.Deleted;
                }
            }
            _context.SaveChanges();
        }

        public Schedule GetScheduleForDate(int clinicId, DateTime date)
        {
            Guid scheduleId = GetScheduleIdForClinic(clinicId);
            
            // populate appointments
            var appointments = _context.Appointments
              .Where(a => a.ScheduleId == scheduleId 
                  && DbFunctions.DiffDays(date, a.TimeRange.Start) == 0)
              .ToList();
            var apptHighlights = GetAppointmentHighlights(date, scheduleId);

            appointments.ForEach(a => a.Title = apptHighlights
                .Where(h => h.Id == a.Id)
                .Select(h => h.Title)
                .FirstOrDefault()
                );

            return new Schedule(scheduleId, DateTimeRange.CreateOneDayRange(date), clinicId, appointments);
        }

        private IEnumerable<AppointmentHighlights> GetAppointmentHighlights(DateTime date, Guid scheduleId)
        {
            // this code can be moved to the database as a stored procedure or view
            var apptHighlightsTsql = @"SELECT A.Id, '('+AT.Code+') ' + rtrim(P.Name) + ' - ' + rtrim(C.FullName) As Title 
                                   FROM            Patients P INNER JOIN
                                                   Appointments A ON P.Id = A.PatientId INNER JOIN
                                                   Clients C ON P.ClientId = C.Id INNER JOIN
                                                   AppointmentTypes AT ON A.AppointmentTypeId = AT.Id
                                   WHERE        (A.ScheduleId = @p0) AND (DATEDIFF(Day, @p1, A.TimeRange_Start) = 0)";

            return _context.Database.SqlQuery<AppointmentHighlights>(apptHighlightsTsql, scheduleId, date).ToList();
        }

        private class AppointmentHighlights
        {
            public Guid Id { get; set; }
            public String Title { get; set; }
        }

        private Guid GetScheduleIdForClinic(int clinicId)
        {
            // gets one schedule entity, and then pulls the id from that tracked EF entity
            // Guid scheduleId = _context.Schedules.FirstOrDefault(s => s.ClinicId == clinicId).Id; 

            // gets just the Guid id from the database
            return _context.Schedules
                .Where(s => s.ClinicId == clinicId)
                .Select(s => s.Id)
                .FirstOrDefault();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
