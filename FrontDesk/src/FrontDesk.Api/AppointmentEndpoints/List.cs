using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorShared.Models.Appointment;
using FrontDesk.Core.Aggregates;
using FrontDesk.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FrontDesk.Api.AppointmentEndpoints
{
    public class List : BaseAsyncEndpoint<ListAppointmentRequest, ListAppointmentResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public List(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("api/appointments")]
        [SwaggerOperation(
            Summary = "List Appointments",
            Description = "List Appointments",
            OperationId = "appointments.List",
            Tags = new[] { "AppointmentEndpoints" })
        ]
        public override async Task<ActionResult<ListAppointmentResponse>> HandleAsync([FromQuery] ListAppointmentRequest request, CancellationToken cancellationToken)
        {
            var response = new ListAppointmentResponse(request.CorrelationId());

            var appointments = await _repository.ListAsync<Appointment, Guid>();
            if (appointments is null) return NotFound();

            response.Appointments = _mapper.Map<List<AppointmentDto>>(appointments);
            response.Count = response.Appointments.Count();

            return Ok(response);
        }
    }
}