using BackEnd.Aplication.DTOs;
using BackEnd.Domain.Interfaces;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repositories;

public class UserNotificationRepository : Repository<UserNotification>, IUserNotificationRepository
{
    public UserNotificationRepository(ToursitDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<IEnumerable<UserNotificationResponseDto>> GetNotificationsByUserIdAsync(int userId,
        bool trackChanges)
    {
        return await FindByCondition(n => n.UserId == userId, trackChanges)
            .Include(n => n.Notification)
            .ThenInclude(t => t.Trip)
            .Select(n => new UserNotificationResponseDto
            {
                NotificationId = n.NotificationId,
                TripId = n.Notification.TripId,
                Title = n.Notification.Title,
                Message = n.Notification.Message,
                IsRead = n.IsRead,
                TripTitle = n.Notification.Trip.Title,
            })
            .ToListAsync();
    }
}