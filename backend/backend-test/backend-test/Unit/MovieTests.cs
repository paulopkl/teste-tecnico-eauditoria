using backend.Controllers;
using backend.Model;
using backend.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace backend_test.Unit
{
    public class MovieTests
    {

        [Fact( DisplayName = "Get All Movies" )]
        [Trait( "Unit", "Movie" )]
        public void GetClients()
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

        [Fact(DisplayName = "Get Movie By Id")]
        [Trait("Unit", "Movie")]
        public void GetOneClient()
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
