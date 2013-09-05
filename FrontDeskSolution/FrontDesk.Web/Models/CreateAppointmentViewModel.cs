using System;
using System.Linq;
using System.Web.Mvc;

namespace FrontDesk.Web.Models
{
    public class CreateAppointmentViewModel
    {
        public Guid PatientId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime DateOfAppointment { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid SelectedDoctor { get; set; }
        public SelectList Doctors { get; set; }
        public string Details { get; set; }
    }

    public class DoctorViewModel
    {
        public Guid DoctorId { get; set; }
        public string Name { get; set; }
    }
}