using System.Security.Claims;
using BackEnd.Aplication.DTOs;
using BackEnd.Domain.Interfaces;
using BackEnd.Domain.Models;

namespace BackEnd.Aplication.Services.Trips;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IUserRepository _userRepository;
    private readonly ICityRepository _cityRepository;
    private readonly ITripNotificationRepository _tripNotificationRepository;
    private readonly IUserNotificationRepository _userNotificationRepository;

    public TripService(ITripRepository tripRepository, IHttpContextAccessor httpContext, IUserRepository userRepository, ICityRepository cityRepository, ITripNotificationRepository tripNotificationRepository, IUserNotificationRepository userNotificationRepository)
    {
        _tripRepository = tripRepository;
        _httpContext = httpContext;
        _userRepository = userRepository;
        _cityRepository = cityRepository;
        _tripNotificationRepository = tripNotificationRepository;
        _userNotificationRepository = userNotificationRepository;
    }

    public async Task<IEnumerable<Trip>> GetAllTripsAsync(bool trackChanges)
    {
        return await _tripRepository.GetAllAsync(trackChanges);
    }

    public async Task<Trip?> GetTripByIdAsync(int id, bool trackChanges)
    {
        return await _tripRepository.GetByIdWithIncludeAsync(id, trackChanges);
    }
    
    public async Task<TripQueryResponseDto<TripResponseDto>> GetTripsByQuery(
        string? searchTitle,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        bool trackChanges)
    {
        return await _tripRepository.GetAllQueryAsync(searchTitle, sortColumn, sortOrder, page, pageSize, trackChanges);
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

        _tripRepository.Create(trip);
        await _tripRepository.SaveChangesAsync();
        
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
            // Users = trip.Users?.Select(t => t.Id).ToList(),
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

        _tripRepository.Update(trip);
        await _tripRepository.SaveChangesAsync();

        if (user.Trips is null)
        {
            user.Trips = new List<Trip> { trip };
        }
        else
        {
            user.Trips.Add(trip);
        }

        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();

        var notifications = await _tripNotificationRepository.GetByTripIdAsync(trip.Id, true);
        if (notifications is not null && notifications.Any())
        {
            foreach (var notification in notifications)
            {
                var userNotification = new UserNotification
                {
                    UserId = userId,
                    NotificationId = notification.Id,
                    IsRead = false
                };
                _userNotificationRepository.Create(userNotification);
            }

            await _userNotificationRepository.SaveChangesAsync();
        }

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
            // Users = trip.Users?.Select(t => t.Id).ToList(),
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
        _tripRepository.Update(trip);
        await _tripRepository.SaveChangesAsync();

        user.Trips?.Remove(trip);
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
        
        var notifications = await _tripNotificationRepository.GetByTripIdAsync(trip.Id, true);
        if (notifications is not null && notifications.Any())
        {
            foreach (var notification in notifications)
            {   
                foreach (var userNotification in notification.UserNotifications)
                {
                    if (userNotification.UserId != userId) continue;
                    _userNotificationRepository.Delete(userNotification);
                    break;
                }
            }

            await _userNotificationRepository.SaveChangesAsync();
        }
        
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
            // Users = trip.Users?.Select(t => t.Id).ToList(),
            CityName = trip.City.Name
        };
    }

    public async Task<TripResponseDto?> DeleteTripAsync(int id)
    {
        var trip = await _tripRepository.GetByIdWithIncludeAsync(id, true);
        if (trip is null) return null;

        _tripRepository.Delete(trip);
        await _tripRepository.SaveChangesAsync();

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
            // Users = trip.Users?.Select(t => t.Id).ToList(),
            CityName = trip.City.Name
        };
    }
}