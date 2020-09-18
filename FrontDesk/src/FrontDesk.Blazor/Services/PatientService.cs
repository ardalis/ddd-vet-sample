using BlazorShared.Models.Patient;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.Blazor.Services
{
    public class PatientService
    {
        private readonly HttpService _httpService;
        private readonly ILogger<PatientService> _logger;

        public PatientService(HttpService httpService, ILogger<PatientService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<PatientDto> Create(PatientDto patient)
        {
            return (await _httpService.HttpPost<CreatePatientResponse>("patients", patient)).Patient;
        }

        public async Task<PatientDto> Edit(PatientDto patient)
        {
            return (await _httpService.HttpPut<UpdatePatientResponse>("patients", patient)).Patient;
        }

        public async Task<string> Delete(int patientId)
        {
            return (await _httpService.HttpDelete<DeletePatientResponse>("patients", patientId)).Status;
        }

        public async Task<PatientDto> GetById(int patientId)
        {
            return (await _httpService.HttpGet<GetByIdPatientResponse>($"patients/{patientId}")).Patient;
        }

        public async Task<List<PatientDto>> ListPaged(int pageSize)
        {
            _logger.LogInformation("Fetching patients from API.");

            return (await _httpService.HttpGet<ListPatientResponse>($"patients")).Patients;
        }

        public async Task<List<PatientDto>> List()
        {
            _logger.LogInformation("Fetching patients from API.");

            return (await _httpService.HttpGet<ListPatientResponse>($"patients")).Patients;
        }
    }
}
