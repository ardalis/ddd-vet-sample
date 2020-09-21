using System;

namespace BlazorShared.Models.Schedule
{
    public class DeleteScheduleResponse : BaseResponse
    {
        public string Status { get; set; } = "Deleted";

        public DeleteScheduleResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteScheduleResponse()
        {
        }
    }
}
