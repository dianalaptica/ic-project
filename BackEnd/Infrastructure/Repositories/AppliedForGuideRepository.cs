using BackEnd.Domain.Interfaces;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repositories;

public class AppliedForGuideRepository : Repository<AppliedForGuide>, IAppliedForGuideRepository
{
    public AppliedForGuideRepository(ToursitDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<IEnumerable<AppliedForGuide>> GetAllAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .ToListAsync();
    }

    public async Task<AppliedForGuide?> GetByIdAsync(int id, bool trackChanges)
    {
        return await FindByCondition(r => r.UserId == id, trackChanges)
            .SingleOrDefaultAsync();
    }
}