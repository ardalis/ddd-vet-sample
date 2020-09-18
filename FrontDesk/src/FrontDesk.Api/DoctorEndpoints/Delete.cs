using System.Threading;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using BlazorShared.Models.Doctor;
using FrontDesk.SharedKernel.Interfaces;
using AutoMapper;
using FrontDesk.Core.Aggregates;

namespace FrontDesk.Api.DoctorEndpoints
{
    public class Delete : BaseAsyncEndpoint<DeleteDoctorRequest, DeleteDoctorResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public Delete(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpDelete("api/doctors/{id}")]
        [SwaggerOperation(
            Summary = "Deletes a Doctor",
            Description = "Deletes a Doctor",
            OperationId = "doctors.delete",
            Tags = new[] { "DoctorEndpoints" })
        ]
        public override async Task<ActionResult<DeleteDoctorResponse>> HandleAsync([FromRoute]DeleteDoctorRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteDoctorResponse(request.CorrelationId());

            var toDelete = _mapper.Map<Doctor>(request);
            await _repository.DeleteAsync<Doctor, int>(toDelete);

            return response;
        }
    }
}
