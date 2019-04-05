using System;
using FrontDesk.SharedKernel;

namespace VetOffice.SharedDatabase.Model
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid ScheduleId { get; set; }
        public int ClientId { get; set; }
        public int PatientId { get; set; }
        public int RoomId { get; set; }
        public int? DoctorId { get; set; }
        public DateTimeRange TimeRange { get; set; }
        public int AppointmentTypeId { get; set; }
        public string Title { get; set; }
        public DateTime? DateTimeConfirmed { get; set; }
    }
}