using backend.Model;
using backend.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly ILogger<ClientController> _logger;
    private IClientRepository _clientRepository;

    public ClientController(ILogger<ClientController> logger, IClientRepository clientRepository)
    {
        _logger = logger;
        _clientRepository = clientRepository;
    }

    /// <summary>
    /// Get a list of all Clients
    /// </summary>
    [HttpGet()]
    [ProducesResponseType(typeof(Client), (int)HttpStatusCode.OK)]
    public IActionResult GetAllClients()
    {
        var result = _clientRepository.FindAll();

        return Ok(result);
    }

    /// <summary>
    /// Get one Client by clientId
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    [HttpGet("{clientId}")]
    [ProducesResponseType(typeof(Client), (int)HttpStatusCode.OK)]
    [ProducesResponseType(404)]
    public IActionResult GetClientById(long clientId)
    {
        var result = _clientRepository.FindById(clientId);

        if (result == null) return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Create one Client
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    [HttpPost()]
    [ProducesResponseType(typeof(ClientRequestResponse), (int)HttpStatusCode.OK)]
    public IActionResult CreateNewClient([FromBody] ClientRequestResponse client)
    {
        var data = new Client { Nome = client.Nome, DataNascimento = client.DataNascimento, CPF = client.CPF };
        var newClient = _clientRepository.CreateClient(data);
        var result = ClientRequestResponse.ConvertToClientResponse(newClient);

        return Ok(result);
    }

    /// <summary>
    /// Update one Client by clientId
    /// </summary>
    /// <param name="client"></param>
    /// <param name="clientId"></param>
    /// <returns></returns>
    [HttpPut("{clientId}")]
    [ProducesResponseType(typeof(ClientRequestResponse), (int)HttpStatusCode.OK)]
    public IActionResult UpdateClient([FromBody] ClientRequestResponse client, long clientId)
    {
        var data = new Client { Nome = client.Nome, DataNascimento = client.DataNascimento, CPF = client.CPF };
        var updateClient = _clientRepository.UpdateClient(clientId, data);
        var result = ClientRequestResponse.ConvertToClientResponse(updateClient);

        return Ok(result);
    }

    /// <summary>
    /// Remove one Client by clientId
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    [HttpDelete("{clientId}")]
    [ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
    public IActionResult RemoveClient(long clientId)
    {
        var result = _clientRepository.DeleteClient(clientId);

        if (result != null)
        {
            return BadRequest(result);
        }

        return Ok(clientId);
    }
}

