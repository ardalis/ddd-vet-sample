using System;
using System.Collections.Generic;

namespace BlazorShared.Models.Client
{
    public class DeleteClientResponse : BaseResponse
    {
        public string Status { get; set; } = "Deleted";

        public DeleteClientResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteClientResponse()
        {
        }
    }
}
