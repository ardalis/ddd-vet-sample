using System;

namespace BlazorShared.Models.Appointment
{
    public class DeleteAppointmentResponse : BaseResponse
    {
        public string Status { get; set; } = "Deleted";

        public DeleteAppointmentResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteAppointmentResponse()
        {
        }
    }
}
