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
}