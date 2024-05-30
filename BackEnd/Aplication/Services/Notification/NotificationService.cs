using System.Security.Claims;
using BackEnd.Aplication.DTOs;
using BackEnd.Domain.Interfaces;
using BackEnd.Domain.Models;

namespace BackEnd.Aplication.Services.Notification;

public class NotificationService : INotificationService
{
    private readonly ITripNotificationRepository _tripNotificationRepository;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IUserNotificationRepository _userNotificationRepository;
    private readonly ITripRepository _tripRepository;

    public NotificationService(ITripNotificationRepository tripNotificationRepository, IHttpContextAccessor httpContext, IUserNotificationRepository userNotificationRepository, ITripRepository tripRepository)
    {
        _tripNotificationRepository = tripNotificationRepository;
        _httpContext = httpContext;
        _userNotificationRepository = userNotificationRepository;
        _tripRepository = tripRepository;
    }

    public async Task<NotificationResponseDto?> GetNotificationByIdAsync(int id, bool trackChanges)
    {
        var result = await _tripNotificationRepository.GetByIdAsync(id, trackChanges);
        if (result == null)
        {
            return null;
        }

        return new NotificationResponseDto
        {
            Id = result.Id,
            TripId = result.TripId,
            Title = result.Title,
            Message = result.Message
        };
    }

    public async Task<IEnumerable<UserNotificationResponseDto>> GetNotificationsByUserIdAsync(bool isUpcoming, bool trackChanges)
    {
        var userId = int.Parse(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        return await _userNotificationRepository.GetNotificationsByUserIdAsync(isUpcoming, userId, trackChanges);
    }

    public async Task<IEnumerable<TripNotificationResponseDto>> GetNotificationsByGuideIdAsync(bool isUpcoming, bool trackChanges)
    {
        var guideId = int.Parse(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        return await _tripNotificationRepository.GetNotificationsByGuideIdAsync(isUpcoming, guideId, trackChanges);
    }

    public async Task<NotificationResponseDto?> CreateNotificationAsync(NotificationCreateDto tripNotificationCreateDto)
    {
        var trip = await _tripRepository.GetByIdAsync(tripNotificationCreateDto.TripId, false);
        if (trip == null)
        {
            return null;
        }

        var tripNotification = new TripNotification
        {
            TripId = tripNotificationCreateDto.TripId,
            Title = tripNotificationCreateDto.Title,
            Message = tripNotificationCreateDto.Message
        };
        
        _tripNotificationRepository.Create(tripNotification);
        await _tripNotificationRepository.SaveChangesAsync();
        var userIds = await _tripRepository.GetAllUserIdsAsync(tripNotification.TripId, false);
        foreach (var id in userIds)
        {
            _userNotificationRepository.Create(new UserNotification
            {
                NotificationId = tripNotification.Id,
                UserId = id,
                IsRead = false
            });
        }
        await _userNotificationRepository.SaveChangesAsync();
        
        return new NotificationResponseDto
        {
            Id = tripNotification.Id,
            TripId = tripNotification.TripId,
            Title = tripNotification.Title,
            Message = tripNotification.Message
        };
    }

    public async Task<NotificationResponseDto?> UpdateReadStatusAsync(int id, bool trackChanges)
    {
        var userId = int.Parse(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var notification = await _tripNotificationRepository.GetByIdAsync(id, trackChanges);
        if (notification == null)
        {
            return null;
        }
        
        var userNotification = notification.UserNotifications.FirstOrDefault(un => un.UserId == userId);
        if (userNotification == null)
        {
            return null;
        }

        userNotification.IsRead = !userNotification.IsRead;
        await _tripRepository.SaveChangesAsync();
        
        return new NotificationResponseDto
        {
            Id = notification.Id,
            TripId = notification.TripId,
            Title = notification.Title,
            Message = notification.Message
        };
    }
    
    public async Task<NotificationResponseDto?> DeleteNotificationAsync(int id)
    {   
        var notification = await _tripNotificationRepository.GetByIdAsync(id, true);
        if (notification == null)
        {
            return null;
        }
        
        _tripNotificationRepository.Delete(notification);
        await _tripNotificationRepository.SaveChangesAsync();
        
        return new NotificationResponseDto
        {
            Id = notification.Id,
            TripId = notification.TripId,
            Title = notification.Title,
            Message = notification.Message
        };
    }
}