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
    public class Delete : BaseAsyncEndpoint<DeleteAppointmentRequest, DeleteAppointmentResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public Delete(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpDelete("api/appointments")]
        [SwaggerOperation(
            Summary = "Deletes a Appointment",
            Description = "Deletes a Appointment",
            OperationId = "appointments.delete",
            Tags = new[] { "AppointmentEndpoints" })
        ]
        public override async Task<ActionResult<DeleteAppointmentResponse>> HandleAsync(DeleteAppointmentRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteAppointmentResponse(request.CorrelationId());

            var toDelete = _mapper.Map<Appointment>(request);
            await _repository.DeleteAsync<Appointment, Guid>(toDelete);

            return response;
        }
    }
}
