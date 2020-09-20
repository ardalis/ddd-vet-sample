using System;

namespace BlazorShared.Models.Appointment
{
    public class UpdateAppointmentRequest : BaseRequest
    {
        public Guid Id { get; set; }
    }
}
