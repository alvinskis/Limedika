using Domain.Entities;

namespace WebUI.Interfaces
{
    public interface IClientDataService
    {
        Task<IEnumerable<Client>?> GetAllClientsAsync();
        Task UploadClientsFromFileAsync();
        Task UpdatePostCodeAsync(Client client);
        Task UpdateClientsAsync(IEnumerable<Client> clients);
    }
}
