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
    private readonly ICityRepository _cityRepository;

    public TripService(ITripRepository tripRepository, IHttpContextAccessor httpContext, IUserRepository userRepository, ICityRepository cityRepository)
    {
        _tripRepository = tripRepository;
        _httpContext = httpContext;
        _userRepository = userRepository;
        _cityRepository = cityRepository;
    }

    public async Task<IEnumerable<Trip>> GetAllTripsAsync(bool trackChanges)
    {
        return await _tripRepository.GetAllAsync(trackChanges);
    }

    public async Task<Trip?> GetTripByIdAsync(int id, bool trackChanges)
    {
        return await _tripRepository.GetByIdWithIncludeAsync(id, trackChanges);
    }

    public async Task<TripResponseDto?> CreateTripAsync(TripCreateDto tripCreateDto)
    {
        var guideId = int.Parse(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var guide = await _userRepository.GetByIdAsync(guideId, true);
        if (guide is null) return null;

        var trip = new Trip
        {
            GuideId = guideId,
            Title = tripCreateDto.Title,
            Description = tripCreateDto.Description,
            Address = tripCreateDto.Adress,
            StartDate = tripCreateDto.StartDate,
            EndDate = tripCreateDto.EndDate,
            MaxTourists = tripCreateDto.MaxTourists,
            CityId = tripCreateDto.CityId
            // TODO: add the rest of the fields
        };

        _tripRepository.CreateTrip(trip);
        await _tripRepository.SaveAsync();
        
        return new TripResponseDto
        {
            Id = trip.Id,
            GuideID = trip.GuideId,
            Title = trip.Title,
            Description = trip.Description,
            Adress = trip.Address,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            MaxTourists = trip.MaxTourists,
            Users = trip.Users?.Select(t => t.Id).ToList(),
            CityName = (await _cityRepository.GetByIdAsync(trip.CityId, false)).Name
        };
    }

    public async Task<TripResponseDto?> JoinTripAsync(int id)
    {
        var trip = await _tripRepository.GetByIdWithIncludeAsync(id, true);
        if (trip is null) return null;
        var limit = trip.MaxTourists;

        var userId = int.Parse(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var user = await _userRepository.GetByIdWithTripsAsync(userId, true);
        if (user is null) return null;
        
        if (trip.Users != null &&
            (trip.Users.Any(t => t.Id == userId) || limit <= trip.Users.Count)) return null;
        
        if (trip.Users is null)
        {
            trip.Users = new List<User> { user };
        }
        else
        {
            trip.Users.Add(user);
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
            GuideID = trip.GuideId,
            Title = trip.Title,
            Description = trip.Description,
            Adress = trip.Address,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            MaxTourists = trip.MaxTourists,
            Users = trip.Users?.Select(t => t.Id).ToList(),
            CityName = trip.City.Name
        };
    }

    public async Task<TripResponseDto?> RemoveTripAsync(int id)
    {
        var trip = await _tripRepository.GetByIdWithIncludeAsync(id, true);
        if (trip is null) return null;
        var limit = trip.MaxTourists;

        var userId = int.Parse(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var user = await _userRepository.GetByIdWithTripsAsync(userId, true);
        if (user is null) return null;

        if (trip.Users == null ||
            trip.Users.Any(t => t.Id == userId) == false) return null;
        
        trip.Users?.Remove(user);
        _tripRepository.UpdateTrip(trip);
        await _tripRepository.SaveAsync();

        user.Trips?.Remove(trip);
        _userRepository.UpdateUser(user);
        await _userRepository.SaveAsync();

        return new TripResponseDto
        {
            Id = trip.Id,
            GuideID = trip.GuideId,
            Title = trip.Title,
            Description = trip.Description,
            Adress = trip.Address,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            MaxTourists = trip.MaxTourists,
            Users = trip.Users?.Select(t => t.Id).ToList(),
            CityName = trip.City.Name
        };
    }

    public async Task<TripResponseDto?> DeleteTripAsync(int id)
    {
        var trip = await _tripRepository.GetByIdWithIncludeAsync(id, true);
        if (trip is null) return null;

        _tripRepository.DeleteTrip(trip);
        await _tripRepository.SaveAsync();

        return new TripResponseDto
        {
            Id = trip.Id,
            GuideID = trip.GuideId,
            Title = trip.Title,
            Description = trip.Description,
            Adress = trip.Address,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            MaxTourists = trip.MaxTourists,
            Users = trip.Users?.Select(t => t.Id).ToList(),
            CityName = trip.City.Name
        };
    }
}