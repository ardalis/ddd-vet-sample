using System.Threading;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using BlazorShared.Models.Schedule;
using FrontDesk.SharedKernel.Interfaces;
using AutoMapper;
using FrontDesk.Core.Aggregates;
using System;

namespace FrontDesk.Api.ScheduleEndpoints
{
    public class Update : BaseAsyncEndpoint<UpdateScheduleRequest, UpdateScheduleResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public Update(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPut("api/schedules")]
        [SwaggerOperation(
            Summary = "Updates a Schedule",
            Description = "Updates a Schedule",
            OperationId = "schedules.update",
            Tags = new[] { "ScheduleEndpoints" })
        ]
        public override async Task<ActionResult<UpdateScheduleResponse>> HandleAsync(UpdateScheduleRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateScheduleResponse(request.CorrelationId());

            var toUpdate = _mapper.Map<Schedule>(request);
            await _repository.UpdateAsync<Schedule, Guid>(toUpdate);

            var dto = _mapper.Map<ScheduleDto>(toUpdate);
            response.Schedule = dto;

            return Ok(response);
        }
    }
}
