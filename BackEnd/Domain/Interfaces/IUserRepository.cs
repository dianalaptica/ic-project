using BackEnd.Domain.Models;

namespace BackEnd.Domain.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(bool trackChanges);
    Task<User?> GetByIdAsync(Guid id, bool trackChanges);
    void CreateUser(User user);
    void DeleteUser(User user);
    Task SaveAsync();
    Task<User?> GetUserByEmail(string email, bool trackChanges);
    Task<User?> GetUserByRefreshToken(string refreshToken, bool trackChanges);
}