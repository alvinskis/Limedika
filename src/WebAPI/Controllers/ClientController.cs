using Application.Interfaces.Infrastructure.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public IActionResult GetClients()
        {
            return Ok(_clientRepository.GetAllClients());
        }

        [HttpPut]
        public IActionResult UpdatePostCode(Client client)
        {
            _clientRepository.UpdatePostCode(client);

            return NoContent();
        }

        [HttpPut("updateClients")]
        public IActionResult UpdateClients(IEnumerable<Client> clients)
        {
            _clientRepository.UpdateClients(clients);

            return NoContent();
        }

    }
}
