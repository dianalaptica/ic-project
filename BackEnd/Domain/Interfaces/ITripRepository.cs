using System.Linq.Expressions;
using BackEnd.Aplication.DTOs;
using BackEnd.Domain.Models;

namespace BackEnd.Domain.Interfaces;

public interface ITripRepository
{
    Task<IEnumerable<Trip>> GetAllAsync(bool trackChanges);
    Task<IEnumerable<Trip>> GetAllTripsForTourist(int id, bool trackChanges);
    Task<IEnumerable<Trip>> GetAllTripsForGuide(int id, bool trackChanges);
    Task<IEnumerable<int>> GetAllUserIdsAsync(int tripId, bool trackChanges);
    Task<Trip?> GetByIdAsync(int id, bool trackChanges);
    Task<Trip?> GetByIdWithIncludeAsync(int id, bool trackChanges);
    Task<TripQueryResponseDto<TripResponseDto>> GetAllQueryAsync(int? cityId,
        string? searchTitle,
        string? sortColumn,
        string? sortOrder,
        bool hasJoined,
        bool isUpcomming,
        string? role,
        int userId,
        int page,
        int pageSize,
        bool trackChanges);
    void Create(Trip trip);
    void Delete(Trip trip);
    void Update(Trip trip);
    Task<int> SaveChangesAsync();
}