using backend.Controllers;
using backend.Model;
using backend.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace backend_test.Unit
{
    public class ClientTests
    {

        [Fact( DisplayName = "Get All Clients" )]
        [Trait( "Unit", "Client" )]
        public void GetClients()
        {
            List<Client> clients = new List<Client>() 
            {
                new Client() { }
            };

            Mock<IClientRepository> clientRepoMock = new();

            clientRepoMock
                .Setup((x) => x.FindAll())
                .Returns(clients);

            var controller = new ClientController(null, clientRepoMock.Object);

            var ret = controller.GetAllClients();

            ret.Should().BeOfType<OkObjectResult>()
                .Which.Value
                .As<List<Client>>();
        }

        [Fact(DisplayName = "Get One Client")]
        [Trait("Unit", "Client")]
        public void GetOneClient()
        {
            Client client = new() { };

            Mock<IClientRepository> clientRepoMock = new();

            clientRepoMock
                .Setup((x) => x.FindById(It.IsAny<long>()))
                .Returns(client);

            var controller = new ClientController(null, clientRepoMock.Object);

            var ret = controller.GetClientById(It.IsAny<long>());

            ret.Should().BeOfType<OkObjectResult>()
                .Which.Value
                .As<Client>();
        }

        [Fact(DisplayName = "Create Client")]
        [Trait("Unit", "Client")]
        public void CreateClient()
        {
            Client client = new() { };
            ClientRequestResponse newClient = ClientRequestResponse.ConvertToClientResponse(client);

            Mock<IClientRepository> clientRepoMock = new();

            clientRepoMock
                .Setup((x) => x.CreateClient(It.IsAny<Client>()))
                .Returns(client);

            var controller = new ClientController(null, clientRepoMock.Object);

            var ret = controller.CreateNewClient(newClient);

            ret.Should().BeOfType<OkObjectResult>()
                .Which
                .Value
                .As<ClientRequestResponse>();
        }

        [Fact(DisplayName = "Update Client By Id")]
        [Trait("Unit", "Client")]
        public void UpdateClient()
        {
            Client client = new() { };
            ClientRequestResponse newClient = ClientRequestResponse.ConvertToClientResponse(client);

            Mock<IClientRepository> clientRepoMock = new();

            clientRepoMock
                .Setup((x) => x.UpdateClient(It.IsAny<long>(), It.IsAny<Client>()))
                .Returns(client);

            var controller = new ClientController(null, clientRepoMock.Object);

            var ret = controller.UpdateClient(newClient, client.Id);

            ret.Should().BeOfType<OkObjectResult>()
                .Which
                .Value
                .As<ClientRequestResponse>();
        }

        [Fact(DisplayName = "Remove Client By Id")]
        [Trait("Unit", "Client")]
        public void RemoveClient()
        {
            Mock<IClientRepository> clientRepoMock = new();

            clientRepoMock
                .Setup((x) => x.DeleteClient(It.IsAny<long>()));

            var controller = new ClientController(null, clientRepoMock.Object);

            var ret = controller.RemoveClient(It.IsAny<long>());

            ret.Should().BeOfType<OkObjectResult>()
                .Which
                .Value
                .As<long>();
        }
    }
}
