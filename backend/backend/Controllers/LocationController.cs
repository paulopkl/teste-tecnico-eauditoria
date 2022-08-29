using backend.Model;
using backend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private ILocationRepository _locationRepository;

        public LocationController(ILogger<LocationController> logger, ILocationRepository locationRepository)
        {
            _logger = logger;
            _locationRepository = locationRepository;
        }

        /// <summary>
        /// Get all Locations formatted with client and movie
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetAllLocations()
        {
            var location = _locationRepository.FindAllJoin();

            return Ok(location);
        }

        /// <summary>
        /// Get one Location By locationId formatted with client and movie
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        [HttpGet("{locationId}")]
        public IActionResult GetLocationById([FromRoute] long locationId)
        {
            var location = _locationRepository.FindbyIdJoin(locationId);

            return Ok(location);
        }

        /// <summary>
        /// Create one Location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult CreateLocation([FromBody] LocationCreation location)
        {
            var data = new Location
            {
                ClientId = location.ClientId,
                MovieId = location.MovieId,
                DataLocacao = location.DataLocacao
            };

            var newLocation = _locationRepository.CreateLocation(data);

            var result = _locationRepository.FindbyIdJoin(newLocation.Id);

            return Ok(result);
        }

        /// <summary>
        /// Update one Location by locationId
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpPut("{locationId}")]
        public IActionResult UpdateLocation([FromRoute] long locationId, [FromBody] LocationRequest location)
        {
            var data = new Location
            {
                ClientId = location.ClientId,
                MovieId = location.MovieId,
                DataLocacao = location.DataLocacao,
                DataDevolucao = location.DataDevolucao
            };

            var updateLocation = _locationRepository.UpdateLocation(locationId, data);

            var result = _locationRepository.FindbyIdJoin(updateLocation.Id);

            return Ok(result);
        }

        /// <summary>
        /// Remove a Location by locationId
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        [HttpDelete("{locationId}")]
        public IActionResult RemoveLocation([FromRoute] long locationId)
        {
            var result = _locationRepository.DeleteLocation(locationId);

            if (result != null)
            {
                return BadRequest(result);
            }

            return Ok(locationId);
        }
    }
}
