using System;
using System.Collections.Generic;
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
        public List<SelectListItem> Doctors { get; set; }
        public string Details { get; set; }
    }

    public class DoctorViewModel
    {
        public Guid DoctorId { get; set; }
        public string Name { get; set; }
    }

    public class ClientViewModel
    {
        public ClientViewModel()
        {
            ClientId = Guid.NewGuid();
        }
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public List<PatientViewModel> Patients { get; set; }
    }

    public class PatientViewModel
    {
        public PatientViewModel()
        {
            PatientId = Guid.NewGuid();
        }
        public Guid PatientId { get; set; }
        public String Name { get; set; }
    }
}