﻿using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorShared.Models.Doctor;
using FrontDesk.Core.Aggregates;
using FrontDesk.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace FrontDesk.Api.DoctorEndpoints
{
    public class GetById : BaseAsyncEndpoint<GetByIdDoctorRequest, GetByIdDoctorResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetById(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("api/doctors/{DoctorId}")]
        [SwaggerOperation(
            Summary = "Get a Doctor by Id",
            Description = "Gets a Doctor by Id",
            OperationId = "doctors.GetById",
            Tags = new[] { "DoctorEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdDoctorResponse>> HandleAsync([FromRoute] GetByIdDoctorRequest request, CancellationToken cancellationToken)
        {
            var response = new GetByIdDoctorResponse(request.CorrelationId());

            var doctor = await _repository.GetByIdAsync<Doctor, int>(request.DoctorId);
            if (doctor is null) return NotFound();

            response.Doctor = _mapper.Map<DoctorDto>(doctor);

            return Ok(response);
        }
    }
    

}