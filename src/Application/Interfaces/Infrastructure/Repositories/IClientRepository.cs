using Domain.Entities;

namespace Application.Interfaces.Infrastructure.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        Client? UpdatePostCode(Client client);
        IEnumerable<Client> UpdateClients(IEnumerable<Client> clients);
    }
}
