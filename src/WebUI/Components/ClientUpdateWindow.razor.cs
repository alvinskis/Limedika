using Domain.Entities;
using Microsoft.AspNetCore.Components;
using WebUI.Interfaces;

namespace WebUI.Components
{
    public partial class ClientUpdateWindow
    {
        [Parameter]
        public Client? Client { get; set; }
        private Client? _client;
        [Parameter]
        public EventCallback OnClose { get; set; }
        [Inject]
        IPostitDataService? PostitDataService { get; set; }
        [Inject]
        IClientDataService? ClientDataService { get; set; }

        protected override void OnParametersSet()
        {
            _client = Client;
        }

        public void CloseWindow()
        {
            OnClose.InvokeAsync();
        }

        public async Task UpdatePostCodeByAddressAsync(Client? client)
        {
            if (client != null && PostitDataService != null && ClientDataService != null)
            {
                client.PostCode = await PostitDataService.GetPostCodeByAddressAsync(client.Address ?? string.Empty);
                await ClientDataService.UpdatePostCodeAsync(client);
            }
        }
    }
}
