using BackEnd.Domain.Models;

namespace BackEnd.Domain.Interfaces;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAllAsync(bool trackChanges);
    Task<Role?> GetByIdAsync(int id, bool trackChanges);
    void Create(Role role);
    void Delete(Role role);
    void Update(Role role);
    Task<int> SaveChangesAsync();
}