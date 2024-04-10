using BackEnd.Domain.Models;

namespace BackEnd.Domain.Interfaces;

public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetAllAsync(bool trackChanges);
    Task<Country?> GetByIdAsync(int id, bool trackChanges);
    void CreateCountry(Country country);
    void DeleteCountry(Country country);
    void UpdateCountry(Country country);
    Task<int> SaveAsync();
}