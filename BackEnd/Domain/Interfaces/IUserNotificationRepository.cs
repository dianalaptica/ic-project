using BackEnd.Aplication.DTOs;
using BackEnd.Domain.Models;

namespace BackEnd.Domain.Interfaces;

public interface IUserNotificationRepository
{
    Task<IEnumerable<UserNotificationResponseDto>> GetNotificationsByUserIdAsync(int userId, bool trackChanges);
    void Create(UserNotification user);
    void Delete(UserNotification user);
    void Update(UserNotification user);
    Task<int> SaveChangesAsync();
}