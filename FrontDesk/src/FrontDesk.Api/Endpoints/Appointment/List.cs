using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorShared.Models.Appointment;
using BlazorShared.Models.AppointmentType;
using FrontDesk.Core.Aggregates;
using FrontDesk.Core.Interfaces;
using FrontDesk.Core.Specifications;
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
        private readonly IApplicationSettings _settings;

        public List(IRepository repository, IMapper mapper, IApplicationSettings settings)
        {
            _repository = repository;
            _mapper = mapper;
            _settings = settings;
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

            var scheduleSpec = new ScheduleForDateAndClinicSpecification(_settings.ClinicId, _settings.TestDate);

            int totalSchedules = await _repository.CountAsync<Schedule, Guid>(scheduleSpec);
            if(totalSchedules <= 0)
            {
                response.Appointments = new List<AppointmentDto>();
                response.Count = 0;
                return Ok(response);
            }

            var schedule = (await _repository.ListAsync<Schedule, Guid>(scheduleSpec)).First();

            var appoinmentSpec = new AppointmentByScheduleIdSpecification(schedule.Id);
            var appointments = (await _repository.ListAsync<Appointment, Guid>(appoinmentSpec)).ToList();

            var myAppointments = _mapper.Map<List<AppointmentDto>>(appointments);

            response.Appointments = myAppointments.OrderBy(a => a.Start).ToList();
            response.Count = response.Appointments.Count();

            return Ok(response);
        }

        private async Task<AppointmentTypeDto> CreateAppointmentTypeAsync(int appointmentTypeId)
        {
            var appointmentType = (await _repository.ListAsync<AppointmentType, int>())?.Where(at => at.Id == appointmentTypeId)?.FirstOrDefault();
            if(appointmentType == null)
            {
                return null;
            }
            return _mapper.Map<AppointmentTypeDto>(appointmentType);
        }

        private async Task<string> GetPatientName(int patientId)
        {
            var patient = (await _repository.ListAsync<Patient, int>())?.Where(at => at.Id == patientId)?.FirstOrDefault();
            if (patient == null) return "None";

            return patient.Name;
        }
    }
}