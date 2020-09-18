using System;

namespace BlazorShared.Models.Doctor
{
    public class DeleteDoctorResponse : BaseResponse
    {
        public DoctorDto Doctor { get; set; } = new DoctorDto();

        public DeleteDoctorResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteDoctorResponse()
        {
        }
    }
}