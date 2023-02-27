using Domain.Entities;
using Microsoft.AspNetCore.Components;
using WebUI.Interfaces;

namespace WebUI.Pages
{
    public partial class ClientsOverview
    {
        [Inject]
        public IClientDataService? ClientDataService { get; set; }

        public List<Client>? Clients { get; set; }
        private Client? _selectedClient;

        protected override async Task OnInitializedAsync()
        {
            if (ClientDataService != null)
            {
                var clients = await ClientDataService.GetAllClientsAsync();

                if (clients != null)
                {
                    Clients = clients.ToList();
                }
            }

        }

        public void UpdatePostCodes()
        {
            _selectedClient = null;
        }

        public void ShowClientUpdateWindow(Client client)
        {
            _selectedClient = client;
        }

        public async Task ImportClientsAsync()
        {
            if (ClientDataService != null)
            {
                await ClientDataService.UploadClientsFromFileAsync();
                var clients = await ClientDataService.GetAllClientsAsync();

                if (clients != null)
                {
                    Clients = clients.ToList();
                }
            }
        }
    }
}
