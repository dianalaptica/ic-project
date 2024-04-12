using BackEnd.Domain.Models;

namespace BackEnd.Domain.Interfaces;

public interface IAppliedForGuideRepository
{
    Task<IEnumerable<AppliedForGuide>> GetAllAsync(bool trackChanges);
    Task<AppliedForGuide?> GetByIdAsync(int id, bool trackChanges);
    void CreateAppliedForGuide(AppliedForGuide appliedForGuide);
    void DeleteAppliedForGuide(AppliedForGuide appliedForGuide);
    void UpdateAppliedForGuide(AppliedForGuide appliedForGuide);
    Task<int> SaveAsync();
}