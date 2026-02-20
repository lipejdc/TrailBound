using Microsoft.AspNetCore.Mvc;
using TrailBound.Application.Dtos.Trip;
using TrailBound.Application.Interfaces;

namespace TrailBound.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController(ITripService tripService) : ControllerBase
    {
        private readonly ITripService _tripService = tripService;

        [HttpGet]
        public async Task<IActionResult> GetAllTrips()
        {
            var trips = await _tripService.GetTripsAsync();
            
            return Ok(trips);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripById(int id)
        {
            var trip = await _tripService.GetTripByIdAsync(id);

            if (trip == null)
            {
                return NotFound();
            }

            return Ok(trip);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip([FromBody] CreateTripDto createTripDto)
        {
            var trip = await _tripService.CreateTripAsync(createTripDto);

            return CreatedAtAction(nameof(GetTripById), new { id = trip.Id }, trip);
        }
    }
}
