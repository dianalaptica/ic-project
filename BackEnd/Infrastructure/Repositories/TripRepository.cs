using System.Linq.Expressions;
using BackEnd.Aplication.DTOs;
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
    
    public async Task<IEnumerable<int>> GetAllUserIdsAsync(int tripId, bool trackChanges)
    {
        return await FindByCondition(t => t.Id == tripId, trackChanges)
            .Include(t => t.Users)
            .SelectMany(t => t.Users.Select(u => u.Id))
            .ToListAsync();
    }
    
    public async Task<Trip?> GetByIdWithIncludeAsync(int id, bool trackChanges)
    {
        return await FindByCondition(t => t.Id == id, trackChanges)
            .Include(t => t.Users)
            .Include(t => t.City)
            .SingleOrDefaultAsync();
    }
    // TODO: maybe not the nicest way to do this
    // TODO: try to refactor this
    public async Task<TripQueryResponseDto<TripResponseDto>> GetAllQueryAsync(
        string? searchTitle,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        bool trackChanges)
    {
        var query = FindAll(trackChanges);
        
        if (!string.IsNullOrWhiteSpace(searchTitle))
        {
            query = query.Where(t => t.Title.Contains(searchTitle));
        }
        
        Expression<Func<Trip, object>> keySelector = sortColumn?.ToLower() switch
        {
            "title" => t => t.Title,
            "city" => t => t.City.Name,
            _ => t => t.Id
        };

        if (sortOrder?.ToLower() == "desc")
        {
            query = query.OrderByDescending(keySelector);
        }
        else
        {
            query = query.OrderBy(keySelector);
        }
        
        var result = await query
            .Include(t => t.City)
            // .Include(c => c.City.Country)
            .Select(t => new TripResponseDto
            {
                Id = t.Id,
                GuideID = t.GuideId,
                Title = t.Title,
                Description = t.Description,
                Adress = t.Address,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                MaxTourists = t.MaxTourists,
                CityName = t.City.Name,
                Image = t.Image
            })
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return new TripQueryResponseDto<TripResponseDto>(result, page, pageSize, await query.CountAsync());
    }
}