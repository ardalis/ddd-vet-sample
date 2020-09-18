using System;

namespace BlazorShared.Models.Appointment
{
    public class UpdateAppointmentRequest : BaseRequest
    {
        public Guid Id { get; set; }
        public Guid ScheduleId { get; set; }
        public int ClientId { get; set; }
        public int PatientId { get; set; }
        public int RoomId { get; set; }
        public int? DoctorId { get; set; }
        public int AppointmentTypeId { get; set; }
    }
}
