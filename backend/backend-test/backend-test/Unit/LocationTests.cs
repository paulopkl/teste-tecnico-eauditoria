using backend.Controllers;
using backend.Model;
using backend.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace backend_test.Unit
{
    public class LocationTests
    {

        [Fact( DisplayName = "Get All Locations")]
        [Trait( "Unit", "Location" )]
        public void GetLocations()
        {
            List<Location> locations = new List<Location>() 
            {
                new Location() { }
            };

            Mock<ILocationRepository> locationRepoMock = new();

            locationRepoMock
                .Setup((x) => x.FindAll())
                .Returns(locations);

            var controller = new LocationController(null, locationRepoMock.Object);

            var ret = controller.GetAllLocations();

            ret.Should().BeOfType<OkObjectResult>()
                .Which
                .Value
                .As<List<Location>>();
        }

        [Fact(DisplayName = "Get Location By Id")]
        [Trait("Unit", "Location")]
        public void GetOneLocation()
        {
            Location location = new() { };
            LocationFormatted locationFormatted = new() { };

            Mock<ILocationRepository> locationRepoMock = new();

            locationRepoMock
                .Setup((x) => x.FindById(It.IsAny<long>()))
                .Returns(location);

            locationRepoMock
                .Setup((x) => x.FindbyIdJoin(It.IsAny<long>()))
                .Returns(locationFormatted);

            var controller = new LocationController(null, locationRepoMock.Object);

            var ret = controller.GetLocationById(It.IsAny<long>());

            ret.Should().BeOfType<OkObjectResult>()
                .Which
                .Value
                .As<LocationFormatted>();
        }

        [Fact(DisplayName = "Update Location By Id")]
        [Trait("Unit", "Location")]
        public void UpdateClient()
        {
            Location location = new() { };
            LocationRequest newLocation = new() { };

            Mock<ILocationRepository> clientRepoMock = new();

            clientRepoMock
                .Setup((x) => x.UpdateLocation(It.IsAny<long>(), It.IsAny<Location>()))
                .Returns(location);

            var controller = new LocationController(null, clientRepoMock.Object);

            var ret = controller.UpdateLocation(location.Id, newLocation);

            ret.Should().BeOfType<OkObjectResult>()
                .Which
                .Value
                .As<ClientRequestResponse>();
        }

        [Fact(DisplayName = "Remove Location By Id")]
        [Trait("Unit", "Location")]
        public void RemoveClient()
        {
            Mock<ILocationRepository> locationRepoMock = new();

            locationRepoMock
                .Setup((x) => x.DeleteLocation(It.IsAny<long>()));

            var controller = new LocationController(null, locationRepoMock.Object);

            var ret = controller.RemoveLocation(It.IsAny<long>());

            ret.Should().BeOfType<OkObjectResult>()
                .Which
                .Value
                .As<long>();
        }
    }
}
