using BackEnd.Aplication.DTOs;

namespace BackEnd.Aplication.Services.Notification;

public interface INotificationService
{
    public Task<NotificationResponseDto?> GetNotificationByIdAsync(int id, bool trackChanges);
    public Task<IEnumerable<UserNotificationResponseDto>> GetNotificationsByUserIdAsync(bool trackChanges);
    public Task<IEnumerable<TripNotificationResponseDto>> GetNotificationsByGuideIdAsync(bool trackChanges);
    public Task<NotificationResponseDto?> CreateNotificationAsync(NotificationCreateDto tripNotificationCreateDto);
    public Task<NotificationResponseDto?> UpdateReadStatusAsync(int id, bool trackChanges);
    public Task<NotificationResponseDto?> DeleteNotificationAsync(int id);
}