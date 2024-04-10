using BackEnd.Domain.Interfaces;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repositories;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(ToursitDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<IEnumerable<Role>> GetAllAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .ToListAsync();
    }

    public async Task<Role?> GetByIdAsync(int id, bool trackChanges)
    {
        return await FindByCondition(r => r.Id == id, trackChanges)
            .SingleOrDefaultAsync();
    }

    public void CreateRole(Role role)
    {
        Create(role);
    }

    public void DeleteRole(Role role)
    {
        Create(role);
    }

    public void UpdateRole(Role role)
    {
        Update(role);
    }

    public async Task<int> SaveAsync()
    {
        return await SaveChangesAsync();
    }
}