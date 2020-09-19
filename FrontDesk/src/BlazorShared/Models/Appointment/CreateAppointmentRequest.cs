using System;

namespace BlazorShared.Models.Appointment
{
    public class CreateAppointmentRequest : BaseRequest
    {
        public int PatientId { get; set; }
        public int ClientId { get; set; }
        public DateTime DateOfAppointment { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid SelectedDoctor { get; set; }
        public string Details { get; set; }
    }
}
