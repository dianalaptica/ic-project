using BackEnd.Domain.Models;

namespace BackEnd.Domain.Interfaces;

public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetAllAsync(bool trackChanges);
    Task<Country?> GetByIdAsync(int id, bool trackChanges);
    void Create(Country country);
    void Delete(Country country);
    void Update(Country country);
    Task<int> SaveChangesAsync();
}