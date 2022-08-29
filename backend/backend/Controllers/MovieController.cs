using backend.Model;
using backend.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly ILogger<MovieController> _logger;
    private IMovieRepository _movieRepository;

    public MovieController(ILogger<MovieController> logger, IMovieRepository movieRepository)
    {
        _logger = logger;
        _movieRepository = movieRepository;
    }

    /// <summary>
    /// Get a list of all movies
    /// </summary>
    [HttpGet()]
    [ProducesResponseType(typeof(List<Movie>), (int)HttpStatusCode.OK)]
    public IActionResult GetAllMovies()
    {
        var result = _movieRepository.FindAll();

        return Ok(result);
    }

    /// <summary>
    /// Get one movie by movieId
    /// </summary>
    /// <param name="movieId"></param>
    /// <returns></returns>
    [HttpGet("{movieId}")]
    [ProducesResponseType(typeof(Movie), (int)HttpStatusCode.OK)]
    [ProducesResponseType(404)]
    public IActionResult GetMovieById(long movieId)
    {
        var result = _movieRepository.FindById(movieId);

        if (result == null) return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// You can upload a list of movies .csv file formatted using ; to separate columns, and save to DB
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost("upload-csv")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public IActionResult UploadCsv(IFormFile file)
    {
        var result = _movieRepository.UploadFileCsv(file);

        if (result != null)
        {
            return BadRequest(result);
        }

        return Ok("Successfully Inserted!");
    }
}
