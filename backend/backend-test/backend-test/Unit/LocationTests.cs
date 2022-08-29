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
            List<Movie> movies = new List<Movie>() 
            {
                new Movie() { }
            };

            Mock<IMovieRepository> movieRepoMock = new();

            movieRepoMock
                .Setup((x) => x.FindAll())
                .Returns(movies);

            var controller = new MovieController(null, movieRepoMock.Object);

            var ret = controller.GetAllMovies();

            ret.Should().BeOfType<OkObjectResult>()
                .Which
                .Value
                .As<List<Movie>>();
        }

        [Fact(DisplayName = "Get Location By Id")]
        [Trait("Unit", "Location")]
        public void GetOneLocation()
        {
            Movie movie = new() { };

            Mock<IMovieRepository> movieRepoMock = new();

            movieRepoMock
                .Setup((x) => x.FindById(It.IsAny<long>()))
                .Returns(movie);

            var controller = new MovieController(null, movieRepoMock.Object);

            var ret = controller.GetMovieById(It.IsAny<long>());

            ret.Should().BeOfType<OkObjectResult>()
                .Which
                .Value
                .As<Movie>();
        }
    }
}
