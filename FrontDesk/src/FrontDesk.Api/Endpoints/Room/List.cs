using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorShared.Models.Room;
using FrontDesk.Core.Aggregates;
using FrontDesk.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FrontDesk.Api.RoomEndpoints
{
    public class List : BaseAsyncEndpoint<ListRoomRequest, ListRoomResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public List(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("api/rooms")]
        [SwaggerOperation(
            Summary = "List Rooms",
            Description = "List Rooms",
            OperationId = "rooms.List",
            Tags = new[] { "RoomEndpoints" })
        ]
        public override async Task<ActionResult<ListRoomResponse>> HandleAsync([FromQuery] ListRoomRequest request, CancellationToken cancellationToken)
        {
            var response = new ListRoomResponse(request.CorrelationId());

            var rooms = await _repository.ListAsync<Room, int>();
            if (rooms is null) return NotFound();

            response.Rooms = _mapper.Map<List<RoomDto>>(rooms);
            response.Count = response.Rooms.Count();

            return Ok(response);
        }
    }
}