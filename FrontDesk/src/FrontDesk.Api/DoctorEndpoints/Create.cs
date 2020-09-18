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
    public class Create : BaseAsyncEndpoint<CreateDoctorRequest, CreateDoctorResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public Create(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("api/doctors")]
        [SwaggerOperation(
            Summary = "Creates a new Doctor",
            Description = "Creates a new Doctor",
            OperationId = "doctors.create",
            Tags = new[] { "DoctorEndpoints" })
        ]
        public override async Task<ActionResult<CreateDoctorResponse>> HandleAsync(CreateDoctorRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateDoctorResponse(request.CorrelationId());

            var toAdd = _mapper.Map<Doctor>(request);
            toAdd = await _repository.AddAsync<Doctor, int>(toAdd);

            var dto = _mapper.Map<DoctorDto>(toAdd);
            response.Doctor = dto;

            return response;
        }
    }
}
