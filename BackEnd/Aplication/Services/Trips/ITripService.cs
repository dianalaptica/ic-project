using System.Linq.Expressions;
using BackEnd.Aplication.DTOs;
using BackEnd.Domain.Models;

namespace BackEnd.Aplication.Services.Trips;

public interface ITripService
{
    public Task<IEnumerable<Trip>> GetAllTripsAsync(bool trackChanges);
    public Task<Trip?> GetTripByIdAsync(Guid id, bool trackChanges);
    public Task<TripResponseDto?> CreateTripAsync(TripCreateDto tripCreateDto);
    public Task<TripResponseDto?> JoinTripAsync(Guid id);
    public Task<TripResponseDto?> RemoveTripAsync(Guid id);
    public Task<TripResponseDto?> DeleteTripAsync(Guid id);
}