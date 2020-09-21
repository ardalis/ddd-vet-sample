using System;

namespace BlazorShared.Models.Doctor
{
    public class DeleteDoctorResponse : BaseResponse
    {
        public string Status { get; set; } = "Deleted";

        public DeleteDoctorResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteDoctorResponse()
        {
        }
    }
}