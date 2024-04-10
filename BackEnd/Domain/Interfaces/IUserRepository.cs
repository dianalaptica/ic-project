using BackEnd.Domain.Models;

namespace BackEnd.Domain.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(bool trackChanges);
    Task<User?> GetByIdAsync(int id, bool trackChanges);
    Task<User?> GetByIdWithTripsAsync(int id, bool trackChanges);
    void CreateUser(User? user);
    void DeleteUser(User user);
    void UpdateUser(User user);
    Task<int> SaveAsync();
    Task<User?> GetUserByEmail(string email, bool trackChanges);
    Task<User?> GetUserByRefreshToken(string refreshToken, bool trackChanges);
    Task<User?> GetUserByPhoneNumber(string phoneNumber, bool trackChanges);
}