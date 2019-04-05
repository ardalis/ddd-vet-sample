using AppointmentScheduling.Core.Interfaces;
using System;
using System.Linq;
using AppointmentScheduling.Core.Model;
using AppointmentScheduling.Core.Model.ScheduleAggregate;

namespace AppointmentScheduling.Data.Repositories
{
    public class AppointmentDTORepository : IAppointmentDTORepository
    {
        private readonly SchedulingContext _context;

        public AppointmentDTORepository(SchedulingContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Loads all details of an appointment.
        /// We cannot load the appointment itself from storage as this is called before the appointment is saved.
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public AppointmentDTO GetFromAppointment(Appointment appointment)
        {
            string query = @"select @p0 'AppointmentId', 
	                            c.FullName 'ClientName', 
	                            c.EmailAddress 'ClientEmailAddress', 
	                            p.Name 'PatientName', 
	                            d.Name 'DoctorName', 
	                            at.Name 'AppointmentType', 
	                            @p1 as Start, 
	                            @p2 as 'End'
                            from Clients c
	                            inner join Patients p on p.Id = @p3
	                            inner join Doctors d on d.Id = @p4
	                            inner join AppointmentTypes at on at.Id = @p5
                            where c.id = @p6
";
            return _context.Database.SqlQuery<AppointmentDTO>(query, appointment.Id, appointment.TimeRange.Start, appointment.TimeRange.End, appointment.PatientId, appointment.DoctorId, appointment.AppointmentTypeId, appointment.ClientId).FirstOrDefault();
        }
    }
}
