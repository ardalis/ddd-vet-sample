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

        public async Task<DoctorDto> Create(CreateDoctorRequest catalogItem)
        {
            return (await _httpService.HttpPost<CreateDoctorResponse>("doctors", catalogItem)).Doctor;
        }

        public async Task<DoctorDto> Edit(DoctorDto catalogItem)
        {
            return (await _httpService.HttpPut<UpdateDoctorResponse>("doctors", catalogItem)).Doctor;
        }

        public async Task<string> Delete(int catalogItemId)
        {
            return (await _httpService.HttpDelete<DeleteDoctorResponse>("doctors", catalogItemId)).Status;
        }

        public async Task<DoctorDto> GetById(int id)
        {
            return (await _httpService.HttpGet<GetByIdDoctorResponse>($"doctors/{id}")).Doctor;
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
