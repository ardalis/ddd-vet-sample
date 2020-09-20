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
    public class Update : BaseAsyncEndpoint<UpdateAppointmentRequest, UpdateAppointmentResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public Update(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPut("api/appointments")]
        [SwaggerOperation(
            Summary = "Updates a Appointment",
            Description = "Updates a Appointment",
            OperationId = "appointments.update",
            Tags = new[] { "AppointmentEndpoints" })
        ]
        public override async Task<ActionResult<UpdateAppointmentResponse>> HandleAsync(UpdateAppointmentRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateAppointmentResponse(request.CorrelationId());

            var toUpdate = _mapper.Map<Appointment>(request);
            await _repository.UpdateAsync<Appointment, Guid>(toUpdate);

            var dto = _mapper.Map<AppointmentDto>(toUpdate);
            response.Appointment = dto;

            return Ok(response);
        }
    }
}
