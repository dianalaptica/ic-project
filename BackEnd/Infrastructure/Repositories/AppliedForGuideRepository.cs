using BackEnd.Aplication.DTOs;
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

    public async Task<IEnumerable<ApplianceResponseDto>> GetAppliancesWithCity(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .Include(c => c.City)
            .Include(co => co.City.Country)
            .Where(a => a.IsApproved == false)
            .Select(a => new ApplianceResponseDto
            {
                UserId = a.UserId,
                CityName = a.City.Name,
                CountryName = a.City.Country.Name,
                IdentityCard = a.IdentityCard,
                IsApproved = a.IsApproved
            })
            .ToListAsync();
    }

    public async Task<AppliedForGuide?> GetByIdAsync(int id, bool trackChanges)
    {
        return await FindByCondition(r => r.UserId == id, trackChanges)
            .SingleOrDefaultAsync();
    }

    public async Task<CityResponseDto?> GetByIdWithCityAsync(int id, bool trackChanges)
    {
        return await FindByCondition(a => a.UserId == id, trackChanges)
            .Include(c => c.City)
            .ThenInclude(co => co.Country)
            .Select(c => new CityResponseDto
            {
                Id = c.CityId,
                CityName = c.City.Name,
                CountryName = c.City.Country.Name
            })
            .SingleOrDefaultAsync();
    }
}