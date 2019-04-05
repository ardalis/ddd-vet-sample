using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FrontDesk.Web.Models
{
    public class CreateAppointmentViewModel
    {
        public int PatientId { get; set; }
        public int ClientId { get; set; }
        public DateTime DateOfAppointment { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid SelectedDoctor { get; set; }
        public string Details { get; set; }
    }

    public class ClientViewModel
    {
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public IEnumerable<PatientViewModel> Patients { get; set; }
    }

    public class PatientViewModel
    {
        public int PatientId { get; set; }
        public String Name { get; set; }
        public int? PreferredDoctorId { get; set; }
    }

    public class AppointmentTypeViewModel
    {
        public int AppointmentTypeId { get; set; }
        public String Name { get; set; }
        public string Code { get; set; }
        public int Duration { get; set; }
    }

    public class AppointmentViewModel
    {
        public Guid AppointmentId { get; set; }
        //public int AppointmentTypeId { get; set; }
        public int RoomId { get; set; }
        public int? DoctorId { get; set; }
        public int ClientId { get; set;  }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public object StartTimezone { get; set; }
        public object EndTimezone { get; set; }
        public object RecurrenceRule { get; set; }
        public object RecurrenceID { get; set; }
        public object RecurrenceException { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsAllDay { get; set; }
        public AppointmentTypeViewModel AppointmentType { get; set; }
        public bool IsPotentiallyConflicting { get; set; }
        public bool IsConfirmed { get; set; }
    }
}