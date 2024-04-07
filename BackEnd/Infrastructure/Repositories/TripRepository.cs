using System.Linq.Expressions;
using BackEnd.Domain.Interfaces;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repositories;

public class TripRepository : Repository<Trip>, ITripRepository
{
    public TripRepository(ApplicationDbContext appDbContext) : base(appDbContext) { }

    public async Task<IEnumerable<Trip>> GetAllAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .ToListAsync();
    }

    public async Task<Trip?> GetByIdAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(t => t.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }
    
    public async Task<Trip?> GetByIdWithUsersAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(t => t.Id.Equals(id), trackChanges)
            .Include(t => t.Tourists)
            .Include(t => t.TouristGuide)
            .SingleOrDefaultAsync();
    }
    
    public void CreateTrip(Trip trip)
    {
        Create(trip);
    }

    public void DeleteTrip(Trip trip)
    {
        Delete(trip);
    }

    public void UpdateTrip(Trip trip)
    {
        Update(trip);
    }

    public async Task SaveAsync()
    {
        await SaveChangesAsync();
    }
}