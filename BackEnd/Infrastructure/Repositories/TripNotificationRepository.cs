using BackEnd.Aplication.DTOs;
using BackEnd.Domain.Interfaces;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repositories;

public class TripNotificationRepository : Repository<TripNotification>, ITripNotificationRepository
{
    public TripNotificationRepository(ToursitDbContext appDbContext) : base(appDbContext)
    {
    }


    public async Task<TripNotification?> GetByIdAsync(int id, bool trackChanges)
    {
        return await FindByCondition(n => n.Id == id, trackChanges)
            .Include(n => n.UserNotifications)
            .SingleOrDefaultAsync();
    }
    
    public async Task<IEnumerable<TripNotification>> GetByTripIdAsync(int tripId, bool trackChanges)
    {
        return await FindByCondition(n => n.TripId == tripId, trackChanges)
            .Include(n => n.UserNotifications)
            .ToListAsync();
    }

    public async Task<IEnumerable<TripNotificationResponseDto>> GetNotificationsByGuideIdAsync(bool isUpcoming, int guideId, bool trackChanges)
    {
        IQueryable<TripNotification> query = FindByCondition(n => n.Trip.GuideId == guideId, trackChanges)
            .Include(n => n.UserNotifications)
            .Include(t => t.Trip);

        if (isUpcoming)
        {
            query = query.Where(t => t.Trip.StartDate >= DateTime.Today);
        }
        else
        {
            query = query.Where(t => t.Trip.StartDate < DateTime.Today);
        }
        
        return await query
            .Select(t => new TripNotificationResponseDto
            {
                Id = t.Id,
                TripId = t.TripId,
                Title = t.Title,
                Message = t.Message,
                TripTitle = t.Trip.Title
            })
            .ToListAsync();
    }
}