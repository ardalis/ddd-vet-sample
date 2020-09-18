using System;

namespace BlazorShared.Models.Patient
{
    public class DeletePatientResponse : BaseResponse
    {
        public PatientDto Patient { get; set; } = new PatientDto();

        public DeletePatientResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeletePatientResponse()
        {
        }
    }
}