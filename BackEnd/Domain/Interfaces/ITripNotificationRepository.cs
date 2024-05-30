using BackEnd.Aplication.DTOs;
using BackEnd.Domain.Models;

namespace BackEnd.Domain.Interfaces;

public interface ITripNotificationRepository
{
    Task<TripNotification?> GetByIdAsync(int id, bool trackChanges);
    Task<IEnumerable<TripNotification>> GetByTripIdAsync(int tripId, bool trackChanges);
    
    Task<IEnumerable<TripNotificationResponseDto>> GetNotificationsByGuideIdAsync(bool isUpcoming, int guideId, bool trackChanges);
    void Create(TripNotification user);
    void Delete(TripNotification user);
    void Update(TripNotification user);
    Task<int> SaveChangesAsync();
}