using Domain.Entities;
using System.Text;
using System.Text.Json;
using WebUI.Interfaces;

namespace WebUI.Services
{
    public class ClientDataService : IClientDataService
    {
        private readonly HttpClient _httpClient;

        public ClientDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Client>?> GetAllClientsAsync()
        {
            var streamObject = await _httpClient.GetStreamAsync("api/client");
            var clients = await JsonSerializer.DeserializeAsync<IEnumerable<Client>?>(streamObject, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (clients == null)
            {
                return null;
            }

            return clients;
        }

        public async Task UploadClientsFromFileAsync()
        {
            var streamObject = await _httpClient.GetStreamAsync("sample-data/customers.json");
            var clients = await JsonSerializer.DeserializeAsync<IEnumerable<Client>>(streamObject, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (clients != null)
            {
                await UpdateClientsAsync(clients);
            }
        }

        public async Task UpdatePostCodeAsync(Client client)
        {
            var clientJson = new StringContent(JsonSerializer.Serialize(client), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/client", clientJson);
        }

        public async Task UpdateClientsAsync(IEnumerable<Client> clients)
        {
            var clientsJson = new StringContent(JsonSerializer.Serialize(clients), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/client/updateClients", clientsJson);
        }
    }
}
