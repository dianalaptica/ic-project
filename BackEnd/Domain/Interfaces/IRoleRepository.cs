using BackEnd.Domain.Models;

namespace BackEnd.Domain.Interfaces;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAllAsync(bool trackChanges);
    Task<Role?> GetByIdAsync(int id, bool trackChanges);
    void CreateRole(Role role);
    void DeleteRole(Role role);
    void UpdateRole(Role role);
    Task<int> SaveAsync();
}