using System.Net.Mime;
using BackEnd.Aplication.DTOs;
using BackEnd.Aplication.Services.Trips;
using BackEnd.Domain.Interfaces;
using BackEnd.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Web.Controllers;

[Route("api/trips")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[Authorize]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripsService;

    public TripsController(ITripService tripsService)
    {
        _tripsService = tripsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Trip>>> GetTrips()
    {
        var trips = await _tripsService.GetAllTripsAsync(false);
        return Ok(trips);
    }
    
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Trip>> GetTrip([FromRoute]int id)
    {
        var trip = await _tripsService.GetTripByIdAsync(id, false);
        if (trip is not null)
        {
            return Ok(trip);
        }

        return NotFound();
    }

    [HttpPost]
    [Authorize(Roles = "Guide")]
    public async Task<ActionResult<Trip>> CreateTrip([FromBody] TripCreateDto trip)
    {
        var result = await _tripsService.CreateTripAsync(trip);
        if (result is not null)
        {
            return CreatedAtAction(nameof(GetTrips), new { id = result.Id }, result);
        }

        return BadRequest();
    }

    [HttpPatch("join/{id}")]
    [Authorize(Roles = "Tourist")]
    public async Task<ActionResult<Trip>> JoinTrip([FromRoute]int id)
    {
        var result = await _tripsService.JoinTripAsync(id);
        if (result is not null)
        {
            return Ok(result);
        }

        return BadRequest();
    }
    
    [HttpPatch("remove/{id}")]
    [Authorize(Roles = "Tourist")]
    public async Task<ActionResult<Trip>> RemoveTrip([FromRoute]int id)
    {
        var result = await _tripsService.RemoveTripAsync(id);
        if (result is not null)
        {
            return Ok(result);
        }

        return BadRequest();
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Guide")]
    public async Task<ActionResult> DeleteTrip([FromRoute]int id)
    {
        var result = await _tripsService.DeleteTripAsync(id);
        if (result is not null)
        {
            return NoContent();
        }

        return BadRequest();
    }
}