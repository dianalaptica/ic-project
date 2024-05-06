using BackEnd.Aplication.DTOs;
using BackEnd.Domain.Models;

namespace BackEnd.Domain.Interfaces;

public interface ITripNotificationRepository
{
    Task<TripNotification?> GetByIdAsync(int id, bool trackChanges);
    void Create(TripNotification user);
    void Delete(TripNotification user);
    void Update(TripNotification user);
    Task<int> SaveChangesAsync();
}