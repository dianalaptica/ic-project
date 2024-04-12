using BackEnd.Domain.Interfaces;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repositories;

public class CityRepository : Repository<City>, ICityRepository
{
    public CityRepository(ToursitDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<IEnumerable<City>> GetAllAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .ToListAsync();
    }

    public async Task<City?> GetByIdAsync(int id, bool trackChanges)
    {
        return await FindByCondition(c => c.Id == id, trackChanges)
            .SingleOrDefaultAsync();
    }
    // TODO: this aplies to every repository make logic nicer, all the crud operations
    // TODO: should be made in the back not implemented every time
    public void CreateCity(City city)
    {
        Create(city);
    }

    public void DeleteCity(City city)
    {
        Delete(city);
    }

    public void UpdateCity(City city)
    {
        Update(city);
    }

    public async Task<int> SaveAsync()
    {
        return await SaveChangesAsync();
    }
}