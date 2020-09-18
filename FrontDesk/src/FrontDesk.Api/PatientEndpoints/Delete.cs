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
    public class Delete : BaseAsyncEndpoint<DeletePatientRequest, DeletePatientResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public Delete(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpDelete("api/patients/{id}")]
        [SwaggerOperation(
            Summary = "Deletes a Patient",
            Description = "Deletes a Patient",
            OperationId = "patients.delete",
            Tags = new[] { "PatientEndpoints" })
        ]
        public override async Task<ActionResult<DeletePatientResponse>> HandleAsync([FromRoute]DeletePatientRequest request, CancellationToken cancellationToken)
        {
            var response = new DeletePatientResponse(request.CorrelationId());

            var toDelete = _mapper.Map<Patient>(request);
            await _repository.DeleteAsync<Patient, int>(toDelete);

            return response;
        }
    }
}
