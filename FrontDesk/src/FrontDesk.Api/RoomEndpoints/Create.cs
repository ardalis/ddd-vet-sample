using System.Threading;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using BlazorShared.Models.Room;
using FrontDesk.SharedKernel.Interfaces;
using AutoMapper;
using FrontDesk.Core.Aggregates;

namespace FrontDesk.Api.RoomEndpoints
{
    public class Create : BaseAsyncEndpoint<CreateRoomRequest, CreateRoomResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public Create(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("api/rooms")]
        [SwaggerOperation(
            Summary = "Creates a new Room",
            Description = "Creates a new Room",
            OperationId = "rooms.create",
            Tags = new[] { "RoomEndpoints" })
        ]
        public override async Task<ActionResult<CreateRoomResponse>> HandleAsync(CreateRoomRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateRoomResponse(request.CorrelationId());

            var toAdd = _mapper.Map<Room>(request);
            toAdd = await _repository.AddAsync<Room, int>(toAdd);

            var dto = _mapper.Map<RoomDto>(toAdd);
            response.Room = dto;

            return response;
        }
    }
}
