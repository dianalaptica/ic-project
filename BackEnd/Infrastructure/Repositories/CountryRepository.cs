using BackEnd.Domain.Interfaces;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repositories;

public class CountryRepository : Repository<Country>, ICountryRepository
{
    public CountryRepository(ToursitDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<IEnumerable<Country>> GetAllAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .ToListAsync();
    }

    public async Task<Country?> GetByIdAsync(int id, bool trackChanges)
    {
        return await FindByCondition(t => t.Id == id, trackChanges)
            .SingleOrDefaultAsync();
    }
}