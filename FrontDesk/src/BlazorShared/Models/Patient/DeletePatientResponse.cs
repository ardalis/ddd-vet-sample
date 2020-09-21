using System;

namespace BlazorShared.Models.Patient
{
    public class DeletePatientResponse : BaseResponse
    {
        public string Status { get; set; } = "Deleted";

        public DeletePatientResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeletePatientResponse()
        {
        }
    }
}