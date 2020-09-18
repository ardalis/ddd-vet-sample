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
    public class Delete : BaseAsyncEndpoint<DeleteRoomRequest, DeleteRoomResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public Delete(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpDelete("api/rooms/{id}")]
        [SwaggerOperation(
            Summary = "Deletes a Room",
            Description = "Deletes a Room",
            OperationId = "rooms.delete",
            Tags = new[] { "RoomEndpoints" })
        ]
        public override async Task<ActionResult<DeleteRoomResponse>> HandleAsync([FromRoute]DeleteRoomRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteRoomResponse(request.CorrelationId());

            var toDelete = _mapper.Map<Room>(request);
            await _repository.DeleteAsync<Room, int>(toDelete);

            return response;
        }
    }
}
