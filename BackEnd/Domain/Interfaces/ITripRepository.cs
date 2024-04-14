using System.Linq.Expressions;
using BackEnd.Aplication.DTOs;
using BackEnd.Domain.Models;

namespace BackEnd.Domain.Interfaces;

public interface ITripRepository
{
    Task<IEnumerable<Trip>> GetAllAsync(bool trackChanges);
    Task<Trip?> GetByIdAsync(int id, bool trackChanges);
    Task<Trip?> GetByIdWithIncludeAsync(int id, bool trackChanges);
    Task<TripQueryResponseDto<TripResponseDto>> GetAllQueryAsync(
        string? searchTitle,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        bool trackChanges);
    void Create(Trip trip);
    void Delete(Trip trip);
    void Update(Trip trip);
    Task<int> SaveChangesAsync();
}