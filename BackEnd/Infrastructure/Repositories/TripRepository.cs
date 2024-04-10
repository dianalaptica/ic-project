using System.Linq.Expressions;
using BackEnd.Domain.Interfaces;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repositories;

public class TripRepository : Repository<Trip>, ITripRepository
{
    public TripRepository(ToursitDbContext appDbContext) : base(appDbContext) { }

    public async Task<IEnumerable<Trip>> GetAllAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .ToListAsync();
    }

    public async Task<Trip?> GetByIdAsync(int id, bool trackChanges)
    {
        return await FindByCondition(t => t.Id == id, trackChanges)
            .SingleOrDefaultAsync();
    }
    
    public async Task<Trip?> GetByIdWithIncludeAsync(int id, bool trackChanges)
    {
        return await FindByCondition(t => t.Id == id, trackChanges)
            .Include(t => t.Users)
            .Include(t => t.City)
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

    public async Task<int> SaveAsync()
    {
        return await SaveChangesAsync();
    }
}