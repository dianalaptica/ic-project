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

    public async Task<IEnumerable<TripResponseDto>> GetAllTripsAsync(bool trackChanges)
    {
        var trip = await _tripRepository.GetAllAsync(trackChanges);
        return trip.Select(t => new TripResponseDto
        {
            Id = t.Id,
            GuideID = t.GuideId,
            Title = t.Title,
            Description = t.Description,
            Adress = t.Address,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            MaxTourists = t.MaxTourists,
            Image = t.Image,
            // Users = t.Users?.Select(u => u.Id).ToList(),
            CityName = (t.City is not null) ? t.City.Name : string.Empty
        }).ToList();
    }

    public async Task<TripResponseDto?> GetTripByIdAsync(int id, bool trackChanges)
    {
        var trip = await _tripRepository.GetByIdWithIncludeAsync(id, trackChanges);
        return new TripResponseDto
        {
            Id = trip?.Id ?? 0,
            GuideID = trip?.GuideId ?? 0,
            Title = trip?.Title ?? string.Empty,
            Description = trip?.Description ?? string.Empty,
            Adress = trip?.Address ?? string.Empty,
            StartDate = trip?.StartDate ?? new DateTime(),
            EndDate = trip?.EndDate ?? new DateTime(),
            MaxTourists = trip?.MaxTourists ?? 0,
            Image = trip?.Image,
            // Users = trip?.Users?.Select(t => t.Id).ToList(),
            CityName = (trip?.City is not null) ? trip.City.Name : string.Empty
        };
    }
    
    public async Task<TripQueryResponseDto<TripResponseDto>> GetTripsByQuery(int? cityId,
        string? searchTitle,
        string? sortColumn,
        string? sortOrder,
        bool hasJoined,
        int page,
        int pageSize,
        bool trackChanges)
    {
        var userId = int.Parse(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        return await _tripRepository.GetAllQueryAsync(cityId, searchTitle, sortColumn, sortOrder, hasJoined, userId,page, pageSize, trackChanges);
    }

    public async Task<TripResponseDto?> CreateTripAsync(TripCreateDto tripCreateDto, byte[] image)
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
            CityId = tripCreateDto.CityId,
            Image = image
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
            Image = trip.Image,
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
        trip.MaxTourists--;

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
            Image = trip.Image,
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
        trip.MaxTourists++;
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