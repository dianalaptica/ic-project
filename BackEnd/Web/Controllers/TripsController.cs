using System.Net.Mime;
using BackEnd.Aplication.DTOs;
using BackEnd.Aplication.Services.Trips;
using BackEnd.Domain.Interfaces;
using BackEnd.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
    [AllowAnonymous]
    public async Task<ActionResult<TripQueryResponseDto<TripResponseDto>>> GetTrips(
        [FromQuery] string? searchTitle,
        [FromQuery] string? sortColumn,
        [FromQuery] string? sortOrder,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 100)
    {
        var result = await _tripsService.GetTripsByQuery(searchTitle, sortColumn, sortOrder, page, pageSize, false);
        if (!result.Trips.IsNullOrEmpty())
        {
            return Ok(result);
        }
        return NotFound();
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "Guide, Tourist")]
    public async Task<ActionResult<Trip>> GetTripById([FromRoute]int id)
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