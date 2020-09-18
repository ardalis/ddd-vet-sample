using System.Threading;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using BlazorShared.Models.Patient;
using FrontDesk.SharedKernel.Interfaces;
using AutoMapper;
using FrontDesk.Core.Aggregates;

namespace FrontDesk.Api.PatientEndpoints
{
    public class Create : BaseAsyncEndpoint<CreatePatientRequest, CreatePatientResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public Create(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("api/patients")]
        [SwaggerOperation(
            Summary = "Creates a new Patient",
            Description = "Creates a new Patient",
            OperationId = "patients.create",
            Tags = new[] { "PatientEndpoints" })
        ]
        public override async Task<ActionResult<CreatePatientResponse>> HandleAsync(CreatePatientRequest request, CancellationToken cancellationToken)
        {
            var response = new CreatePatientResponse(request.CorrelationId());

            var toAdd = _mapper.Map<Patient>(request);
            toAdd = await _repository.AddAsync<Patient, int>(toAdd);

            var dto = _mapper.Map<PatientDto>(toAdd);
            response.Patient = dto;

            return response;
        }
    }
}
