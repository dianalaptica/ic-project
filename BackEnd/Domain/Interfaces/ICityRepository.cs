using BackEnd.Domain.Models;

namespace BackEnd.Domain.Interfaces;

public interface ICityRepository
{
    Task<IEnumerable<City>> GetAllAsync(bool trackChanges);
    Task<City?> GetByIdAsync(int id, bool trackChanges);
    void CreateCity(City city);
    void DeleteCity(City city);
    void UpdateCity(City city);
    Task<int> SaveAsync();
}