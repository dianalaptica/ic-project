using BackEnd.Aplication.DTOs;
using BackEnd.Domain.Models;

namespace BackEnd.Domain.Interfaces;

public interface IAppliedForGuideRepository
{
    Task<IEnumerable<AppliedForGuide>> GetAllAsync(bool trackChanges);
    Task<IEnumerable<ApplianceResponseDto>> GetAppliancesWithCity(bool trackChanges);
    Task<AppliedForGuide?> GetByIdAsync(int id, bool trackChanges);
    Task<CityResponseDto?> GetByIdWithCityAsync(int id, bool trackChanges);
    void Create(AppliedForGuide appliedForGuide);
    void Delete(AppliedForGuide appliedForGuide);
    void Update(AppliedForGuide appliedForGuide);
    Task<int> SaveChangesAsync();
}