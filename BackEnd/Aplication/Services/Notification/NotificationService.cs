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

    public async Task<IEnumerable<NotificationResponseDto>> GetNotificationsByUserIdAsync(bool trackChanges)
    {
        var userId = int.Parse(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        Console.WriteLine(userId);
        return await _userNotificationRepository.GetNotificationsByUserIdAsync(userId, trackChanges);
    }

    public async Task<NotificationResponseDto?> CreateNotificationAsync(NotificationCreateDto tripNotificationCreateDto)
    {
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

    public Task<NotificationResponseDto?> DeleteNotificationAsync(int id)
    {
        throw new NotImplementedException();
    }
}