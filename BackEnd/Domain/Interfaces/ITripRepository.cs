using System.Linq.Expressions;
using BackEnd.Domain.Models;

namespace BackEnd.Domain.Interfaces;

public interface ITripRepository
{
    Task<IEnumerable<Trip>> GetAllAsync(bool trackChanges);
    Task<Trip?> GetByIdAsync(int id, bool trackChanges);
    Task<Trip?> GetByIdWithIncludeAsync(int id, bool trackChanges);
    void CreateTrip(Trip trip);
    void DeleteTrip(Trip trip);
    void UpdateTrip(Trip trip);
    Task<int> SaveAsync();
}