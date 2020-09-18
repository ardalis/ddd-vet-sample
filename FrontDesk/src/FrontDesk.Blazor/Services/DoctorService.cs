using BlazorShared.Models.Doctor;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.Blazor.Services
{
    public class DoctorService
    {
        private readonly HttpService _httpService;
        private readonly ILogger<DoctorService> _logger;

        public DoctorService(HttpService httpService, ILogger<DoctorService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<DoctorDto> Create(CreateDoctorRequest doctor)
        {
            return (await _httpService.HttpPost<CreateDoctorResponse>("doctors", doctor)).Doctor;
        }

        public async Task<DoctorDto> Edit(DoctorDto doctor)
        {
            return (await _httpService.HttpPut<UpdateDoctorResponse>("doctors", doctor)).Doctor;
        }

        public async Task<string> Delete(int doctorId)
        {
            return (await _httpService.HttpDelete<DeleteDoctorResponse>("doctors", doctorId)).Status;
        }

        public async Task<DoctorDto> GetById(int doctorId)
        {
            return (await _httpService.HttpGet<GetByIdDoctorResponse>($"doctors/{doctorId}")).Doctor;
        }

        public async Task<List<DoctorDto>> ListPaged(int pageSize)
        {
            _logger.LogInformation("Fetching doctors from API.");

            return (await _httpService.HttpGet<ListDoctorResponse>($"doctors")).Doctors;
        }

        public async Task<List<DoctorDto>> List()
        {
            _logger.LogInformation("Fetching doctors from API.");

            return (await _httpService.HttpGet<ListDoctorResponse>($"doctors")).Doctors;
        }
    }
}
