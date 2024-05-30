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

    public async Task<IEnumerable<UserNotificationResponseDto>> GetNotificationsByUserIdAsync(bool isUpcoming, int userId,
        bool trackChanges)
    {
        IQueryable<UserNotification> query = FindByCondition(n => n.UserId == userId, trackChanges)
            .Include(n => n.Notification)
            .ThenInclude(t => t.Trip);

        if (isUpcoming)
        {
            query = query.Where(t => t.Notification.Trip.StartDate >= DateTime.Today);
        }
        else
        {
            query = query.Where(t => t.Notification.Trip.StartDate < DateTime.Today);
        }

        return await query
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