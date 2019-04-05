using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AppointmentScheduling.Core.Model.ScheduleAggregate;
using AppointmentScheduling.Data;
using FrontDesk.SharedKernel;
using FrontDesk.Web.Models;
using System.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using AppointmentScheduling.Core.Interfaces;
using ClientPatientManagement.Data;

namespace FrontDesk.Web.Controllers.Api
{
    public class AppointmentsController : ApiController
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IApplicationSettings _settings;

        public AppointmentsController(IScheduleRepository scheduleRepository, IApplicationSettings settings)
        {
            this._scheduleRepository = scheduleRepository;
            this._settings = settings;
        }

        // GET api/appointments
        public IEnumerable<AppointmentViewModel> Get()
        {
            var schedule = _scheduleRepository.GetScheduleForDate(_settings.ClinicId, _settings.TestDate);

            var myAppointments = schedule.Appointments;
            return myAppointments
                .Select(a => FromAppointment(a))
                .OrderBy(a => a.Start);
        }

        // GET api/appointments/{guid}
        public AppointmentViewModel Get(Guid id)
        {
            var schedule = _scheduleRepository.GetScheduleForDate(_settings.ClinicId, _settings.TestDate);
            var appointment = FromAppointment(schedule.Appointments.Single(a => a.Id == id));
            if(appointment==null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return appointment;
        }

        // POST api/appointments
        public void Post([FromBody]
                         AppointmentViewModel appointment)
        {
            Debug.Print("--> POST: " + JsonConvert.SerializeObject(appointment));
            var schedule = _scheduleRepository.GetScheduleForDate(_settings.ClinicId, _settings.TestDate);

            var newAppointment = Appointment.Create(schedule.Id,
                appointment.ClientId, appointment.PatientId,
                appointment.RoomId, appointment.Start.ToLocalTime(), appointment.End.ToLocalTime(), appointment.AppointmentType.AppointmentTypeId, appointment.DoctorId,
                appointment.Title);
            schedule.AddNewAppointment(newAppointment);
            _scheduleRepository.Update(schedule);
        }

        // PUT api/appointments/{guid}
        public void Put(Guid id, [FromBody]
                        AppointmentViewModel appointment)
        {
            Debug.Print("--> PUT: " + JsonConvert.SerializeObject(appointment));
            var schedule = _scheduleRepository.GetScheduleForDate(_settings.ClinicId, _settings.TestDate);
            var appointmentToUpdate = schedule.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointmentToUpdate == null) { throw new ApplicationException("Appointment not found."); }
            appointmentToUpdate.UpdateRoom(appointment.RoomId);
            var startEnd = new DateTimeRange(appointment.Start.ToLocalTime(), 
                appointment.End.ToLocalTime());
            appointmentToUpdate.UpdateTime(startEnd);
            _scheduleRepository.Update(schedule);
        }

        // DELETE api/appointments/{guid}
        public void Delete(Guid id)
        {
            var schedule = _scheduleRepository.GetScheduleForDate(_settings.ClinicId, _settings.TestDate);
            var appointment = schedule.Appointments.FirstOrDefault(a => a.Id == id);

            schedule.DeleteAppointment(appointment);
            _scheduleRepository.Update(schedule);
        }

        private AppointmentViewModel FromAppointment(Appointment appt)
        {
            var factory = new AppointmentViewModelFactory();
            return factory.CreateFromAppointment(appt);
        }
    }

    public class AppointmentViewModelFactory
    {
        private SchedulingContext db = new SchedulingContext();
        private CrudContext _crudContext = new CrudContext();
        private List<ClientPatientManagement.Core.Model.Patient> _patients = new List<ClientPatientManagement.Core.Model.Patient>();

        private AppointmentTypeViewModel CreateAppointmentType(int id)
        {
            return db.AppointmentTypes
                .Where(at => at.Id == id)
                .Select(at => new AppointmentTypeViewModel() {
                    AppointmentTypeId= at.Id,
                    Code=at.Code,
                    Duration=at.Duration,
                    Name=at.Name
                })
                .Single();
        }
        public AppointmentViewModel CreateFromAppointment(Appointment appt)
        {
            return new AppointmentViewModel()
            {
                AppointmentId = appt.Id,
                AppointmentType = CreateAppointmentType(appt.AppointmentTypeId),
                ClientId = appt.ClientId,
                DoctorId = appt.DoctorId,
                PatientId = appt.PatientId,
                PatientName = GetPatientName(appt.PatientId),
                RoomId = appt.RoomId,
                Start = appt.TimeRange.Start,
                End = appt.TimeRange.End,
                IsAllDay = false,
                Title = appt.Title,
                Description = "No Description",
                IsPotentiallyConflicting =appt.IsPotentiallyConflicting,
                IsConfirmed = appt.DateTimeConfirmed.HasValue
            };
        }

        private string GetPatientName(int patientId)
        {
            if(_patients.Count == 0)
            {
                _patients.AddRange(_crudContext.Patients.AsEnumerable());
            }
            var patient = _patients.FirstOrDefault(p => p.Id == patientId);
            if(patient == null) return "None";
            return patient.Name;
       }
    }
}