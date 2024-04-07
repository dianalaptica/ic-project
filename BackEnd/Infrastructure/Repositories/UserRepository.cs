using BackEnd.Domain.Interfaces;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{   
    public UserRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<User>> GetAllAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(u => u.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }
    
    public async Task<User?> GetByIdWithTripsAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(u => u.Id.Equals(id), trackChanges)
            .Include(u => u.Trips)
            .SingleOrDefaultAsync();
    }

    public void CreateUser(User user)
    {
        Create(user);
    }

    public void DeleteUser(User user)
    {
        Delete(user);
    }
    
    public void UpdateUser(User user)
    {
        Update(user);
    }
    
    public async Task SaveAsync()
    {
        await SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmail(string email, bool trackChanges)
    {
        return await FindByCondition(u => u.Email == email, trackChanges)
            .SingleOrDefaultAsync();
    }
    
    public async Task<User?> GetUserByRefreshToken(string? refreshToken, bool trackChanges)
    {   
        if (refreshToken == null) return null;
        return await FindByCondition(u => u.RefreshToken == refreshToken, trackChanges)
            .SingleOrDefaultAsync();
    }
}