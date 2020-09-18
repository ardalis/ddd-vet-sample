using System;

namespace BlazorShared.Models.Appointment
{
    public class CreateAppointmentRequest : BaseRequest
    {
        public Guid ScheduleId { get; set; }
        public int ClientId { get; set; }
        public int PatientId { get; set; }
        public int RoomId { get; set; }
        public int? DoctorId { get; set; }
        public int AppointmentTypeId { get; set; }
    }
}
