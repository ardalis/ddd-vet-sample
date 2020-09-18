using BlazorShared.Models.AppointmentType;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.Blazor.Services
{
    public class AppointmentTypeService
    {
        private readonly HttpService _httpService;
        private readonly ILogger<AppointmentTypeService> _logger;

        public AppointmentTypeService(HttpService httpService, ILogger<AppointmentTypeService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }  

        public async Task<List<AppointmentTypeDto>> ListPaged(int pageSize)
        {
            _logger.LogInformation("Fetching appointment types from API.");

            return (await _httpService.HttpGet<ListAppointmentTypeResponse>($"appointment-types")).AppointmentTypes;
        }

        public async Task<List<AppointmentTypeDto>> List()
        {
            _logger.LogInformation("Fetching appointment types from API.");

            return (await _httpService.HttpGet<ListAppointmentTypeResponse>($"appointment-types")).AppointmentTypes;
        }
    }
}
