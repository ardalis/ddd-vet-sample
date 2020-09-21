using System;

namespace BlazorShared.Models.Room
{
    public class DeleteRoomResponse : BaseResponse
    {
        public string Status { get; set; } = "Deleted";
        public DeleteRoomResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteRoomResponse()
        {
        }
    }
}