using System.Threading;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using BlazorShared.Models.Appointment;
using FrontDesk.SharedKernel.Interfaces;
using AutoMapper;
using FrontDesk.Core.Aggregates;
using System;

namespace FrontDesk.Api.AppointmentEndpoints
{
    public class Create : BaseAsyncEndpoint<CreateAppointmentRequest, CreateAppointmentResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public Create(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("api/appointments")]
        [SwaggerOperation(
            Summary = "Creates a new Appointment",
            Description = "Creates a new Appointment",
            OperationId = "appointments.create",
            Tags = new[] { "AppointmentEndpoints" })
        ]
        public override async Task<ActionResult<CreateAppointmentResponse>> HandleAsync(CreateAppointmentRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateAppointmentResponse(request.CorrelationId());

            var toAdd = _mapper.Map<Appointment>(request);
            toAdd = await _repository.AddAsync<Appointment, Guid>(toAdd);

            var dto = _mapper.Map<AppointmentDto>(toAdd);
            response.Appointment = dto;

            return Ok(response);
        }
    }
}
