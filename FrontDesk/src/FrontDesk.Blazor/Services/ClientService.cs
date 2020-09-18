using BlazorShared.Models.Client;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.Blazor.Services
{
    public class ClientService
    {
        private readonly HttpService _httpService;
        private readonly ILogger<ClientService> _logger;

        public ClientService(HttpService httpService, ILogger<ClientService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<ClientDto> Create(CreateClientRequest client)
        {
            return (await _httpService.HttpPost<CreateClientResponse>("clients", client)).Client;
        }

        public async Task<ClientDto> Edit(ClientDto client)
        {
            return (await _httpService.HttpPut<UpdateClientResponse>("clients", client)).Client;
        }

        public async Task<string> Delete(int clientId)
        {
            return (await _httpService.HttpDelete<DeleteClientResponse>("clients", clientId)).Status;
        }

        public async Task<ClientDto> GetById(int clientId)
        {
            return (await _httpService.HttpGet<GetByIdClientResponse>($"clients/{clientId}")).Client;
        }

        public async Task<List<ClientDto>> ListPaged(int pageSize)
        {
            _logger.LogInformation("Fetching clients from API.");

            return (await _httpService.HttpGet<ListClientResponse>($"clients")).Clients;
        }

        public async Task<List<ClientDto>> List()
        {
            _logger.LogInformation("Fetching clients from API.");

            return (await _httpService.HttpGet<ListClientResponse>($"clients")).Clients;
        }
    }
}
