using Application.Interfaces.Infrastructure.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _dataContext;

        public ClientRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _dataContext.Clients;
        }

        public Client? UpdatePostCode(Client client)
        {
            var foundClient = _dataContext.Clients?.FirstOrDefault(c => c.Id == client.Id);

            if (foundClient == null)
            {
                return null;
            }

            foundClient.PostCode = client.PostCode;
            _dataContext.SaveChanges();
            return foundClient;
        }

        public IEnumerable<Client> UpdateClients(IEnumerable<Client> clients) 
        {
            var clientsToAdd = new List<Client>();

            foreach (var client in clients)
            {
                if (_dataContext.Clients?.FirstOrDefault(c => c.Address.Equals(client.Address)) == null)
                {
                    clientsToAdd.Add(client);
                }
            }

            using (_dataContext)
            {
                _dataContext.Clients.AddRange(clientsToAdd);
                _dataContext.SaveChanges();
            }
            
            return _dataContext.Clients;
        }
    }
}
