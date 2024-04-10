using BackEnd.Domain.Models;

namespace BackEnd.Domain.Interfaces;

public interface ICityRepository
{
    Task<IEnumerable<City>> GetAllAsync(bool trackChanges);
    Task<City?> GetByIdAsync(int id, bool trackChanges);
    void Create(City city);
    void Delete(City city);
    void Update(City city);
    Task<int> SaveChangesAsync();
}