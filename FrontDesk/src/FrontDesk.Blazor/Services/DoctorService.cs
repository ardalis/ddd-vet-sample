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

        public async Task<DoctorDto> CreateAsync(DoctorDto doctor)
        {
            return (await _httpService.HttpPostAsync<CreateDoctorResponse>("doctors", doctor)).Doctor;
        }

        public async Task<DoctorDto> EditAsync(DoctorDto doctor)
        {
            return (await _httpService.HttpPutAsync<UpdateDoctorResponse>("doctors", doctor)).Doctor;
        }

        public async Task<string> DeleteAsync(int doctorId)
        {
            return (await _httpService.HttpDeleteAsync<DeleteDoctorResponse>("doctors", doctorId)).Status;
        }

        public async Task<DoctorDto> GetByIdAsync(int doctorId)
        {
            return (await _httpService.HttpGetAsync<GetByIdDoctorResponse>($"doctors/{doctorId}")).Doctor;
        }

        public async Task<List<DoctorDto>> ListPagedAsync(int pageSize)
        {
            _logger.LogInformation("Fetching doctors from API.");

            return (await _httpService.HttpGetAsync<ListDoctorResponse>($"doctors")).Doctors;
        }

        public async Task<List<DoctorDto>> ListAsync()
        {
            _logger.LogInformation("Fetching doctors from API.");

            return (await _httpService.HttpGetAsync<ListDoctorResponse>($"doctors")).Doctors;
        }
    }
}
