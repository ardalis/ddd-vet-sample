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
    public class Create : BaseAsyncEndpoint<CreateScheduleRequest, CreateScheduleResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public Create(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("api/schedules")]
        [SwaggerOperation(
            Summary = "Creates a new Schedule",
            Description = "Creates a new Schedule",
            OperationId = "schedules.create",
            Tags = new[] { "ScheduleEndpoints" })
        ]
        public override async Task<ActionResult<CreateScheduleResponse>> HandleAsync(CreateScheduleRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateScheduleResponse(request.CorrelationId());

            var toAdd = _mapper.Map<Schedule>(request);
            toAdd = await _repository.AddAsync<Schedule, Guid>(toAdd);

            var dto = _mapper.Map<ScheduleDto>(toAdd);
            response.Schedule = dto;

            return response;
        }
    }
}
