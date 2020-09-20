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
    public class Update : BaseAsyncEndpoint<UpdateDoctorRequest, UpdateDoctorResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public Update(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPut("api/doctors")]
        [SwaggerOperation(
            Summary = "Updates a Doctor",
            Description = "Updates a Doctor",
            OperationId = "doctors.update",
            Tags = new[] { "DoctorEndpoints" })
        ]
        public override async Task<ActionResult<UpdateDoctorResponse>> HandleAsync(UpdateDoctorRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateDoctorResponse(request.CorrelationId());

            var toUpdate = _mapper.Map<Doctor>(request);
            await _repository.UpdateAsync<Doctor, int>(toUpdate);

            var dto = _mapper.Map<DoctorDto>(toUpdate);
            response.Doctor = dto;

            return Ok(response);
        }
    }
}
