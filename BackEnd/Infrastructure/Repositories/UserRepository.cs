using BackEnd.Domain.Interfaces;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{   
    public UserRepository(ToursitDbContext context) : base(context) { }

    public async Task<IEnumerable<User>> GetAllAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id, bool trackChanges)
    {
        return await FindByCondition(u => u.Id == id, trackChanges)
            .SingleOrDefaultAsync();
    }
    
    public async Task<User?> GetByIdWithTripsAsync(int id, bool trackChanges)
    {
        return await FindByCondition(u => u.Id == id, trackChanges)
            .Include(u => u.Trips)
            .SingleOrDefaultAsync();
    }

    public void CreateUser(User? user)
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
    
    public async Task<int> SaveAsync()
    {
        return await SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmail(string email, bool trackChanges)
    {
        return await FindByCondition(u => u.Email == email, trackChanges)
            .Include(u => u.Role)
            .SingleOrDefaultAsync();
    }
    
    public async Task<User?> GetUserByRefreshToken(string? refreshToken, bool trackChanges)
    {   
        if (refreshToken == null) return null;
        return await FindByCondition(u => u.RefreshToken == refreshToken, trackChanges)
            .SingleOrDefaultAsync();
    }

    public async Task<User?> GetUserByPhoneNumber(string phoneNumber, bool trackChanges)
    {
        return await FindByCondition(u => u.PhoneNumber == phoneNumber, trackChanges)
            .SingleOrDefaultAsync();
    }
}