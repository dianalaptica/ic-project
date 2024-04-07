using System.Security.Claims;
using BackEnd.Aplication.DTOs;
using BackEnd.Domain.Interfaces;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace BackEnd.Aplication.Services.Trips;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IUserRepository _userRepository;

    public TripService(ITripRepository tripRepository, IHttpContextAccessor httpContext, IUserRepository userRepository)
    {
        _tripRepository = tripRepository;
        _httpContext = httpContext;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Trip>> GetAllTripsAsync(bool trackChanges)
    {
        return await _tripRepository.GetAllAsync(trackChanges);
    }

    public async Task<Trip?> GetTripByIdAsync(Guid id, bool trackChanges)
    {
        return await _tripRepository.GetByIdWithUsersAsync(id, trackChanges);
    }

    public async Task<TripResponseDto?> CreateTripAsync(TripCreateDto tripCreateDto)
    {
        var guideId = Guid.Parse(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var guide = await _userRepository.GetByIdAsync(guideId, true);
        if (guide is null) return null;

        var trip = new Trip
        {
            Id = Guid.NewGuid(),
            TouristGuide = guide,
            Title = tripCreateDto.Title,
            Description = tripCreateDto.Description,
            Location = tripCreateDto.Location,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today,
            MaxTourists = tripCreateDto.MaxTourists,
            Tourists = null
        };

        _tripRepository.CreateTrip(trip);
        await _tripRepository.SaveAsync();

        return new TripResponseDto
        {
            Id = trip.Id,
            TouristGuide = trip.TouristGuide.Id,
            Title = trip.Title,
            Description = trip.Description,
            Location = trip.Location,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            MaxTourists = trip.MaxTourists,
            Tourists = trip.Tourists?.Select(t => t.Id).ToList()
        };
    }

    public async Task<TripResponseDto?> JoinTripAsync(Guid id)
    {
        var trip = await _tripRepository.GetByIdWithUsersAsync(id, true);
        if (trip is null) return null;
        var limit = trip.MaxTourists;

        var userId = Guid.Parse(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var user = await _userRepository.GetByIdWithTripsAsync(userId, true);
        if (user is null) return null;
        
        if (trip.Tourists != null &&
            (trip.Tourists.Any(t => t.Id == userId) || limit <= trip.Tourists.Count)) return null;
        
        if (trip.Tourists is null)
        {
            trip.Tourists = new List<User> { user };
        }
        else
        {
            trip.Tourists.Add(user);
        }

        _tripRepository.UpdateTrip(trip);
        await _tripRepository.SaveAsync();

        if (user.Trips is null)
        {
            user.Trips = new List<Trip> { trip };
        }
        else
        {
            user.Trips.Add(trip);
        }

        _userRepository.UpdateUser(user);
        await _userRepository.SaveAsync();

        return new TripResponseDto
        {
            Id = trip.Id,
            TouristGuide = trip.TouristGuide.Id,
            Title = trip.Title,
            Description = trip.Description,
            Location = trip.Location,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            MaxTourists = trip.MaxTourists,
            Tourists = trip.Tourists?.Select(t => t.Id).ToList()
        };
    }

    public async Task<TripResponseDto?> RemoveTripAsync(Guid id)
    {
        var trip = await _tripRepository.GetByIdWithUsersAsync(id, true);
        if (trip is null) return null;
        var limit = trip.MaxTourists;

        var userId = Guid.Parse(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var user = await _userRepository.GetByIdWithTripsAsync(userId, true);
        if (user is null) return null;

        if (trip.Tourists == null ||
            trip.Tourists.Any(t => t.Id == userId) == false) return null;
        
        trip.Tourists?.Remove(user);
        _tripRepository.UpdateTrip(trip);
        await _tripRepository.SaveAsync();

        user.Trips?.Remove(trip);
        _userRepository.UpdateUser(user);
        await _userRepository.SaveAsync();

        return new TripResponseDto
        {
            Id = trip.Id,
            TouristGuide = trip.TouristGuide.Id,
            Title = trip.Title,
            Description = trip.Description,
            Location = trip.Location,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            MaxTourists = trip.MaxTourists,
            Tourists = trip.Tourists?.Select(t => t.Id).ToList()
        };
    }

    public async Task<TripResponseDto?> DeleteTripAsync(Guid id)
    {
        var trip = await _tripRepository.GetByIdWithUsersAsync(id, true);
        if (trip is null) return null;

        _tripRepository.DeleteTrip(trip);
        await _tripRepository.SaveAsync();

        return new TripResponseDto
        {
            Id = trip.Id,
            TouristGuide = trip.TouristGuide.Id,
            Title = trip.Title,
            Description = trip.Description,
            Location = trip.Location,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            MaxTourists = trip.MaxTourists,
            Tourists = trip.Tourists?.Select(t => t.Id).ToList()
        };
    }
}