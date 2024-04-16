using System.Linq.Expressions;
using BackEnd.Aplication.DTOs;
using BackEnd.Domain.Models;

namespace BackEnd.Aplication.Services.Trips;

public interface ITripService
{
    public Task<IEnumerable<Trip>> GetAllTripsAsync(bool trackChanges);
    public Task<Trip?> GetTripByIdAsync(int id, bool trackChanges);
    public Task<TripQueryResponseDto<TripResponseDto>> GetTripsByQuery(
        string? searchTitle,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        bool trackChanges);
    public Task<TripResponseDto?> CreateTripAsync(TripCreateDto tripCreateDto);
    public Task<TripResponseDto?> JoinTripAsync(int id);
    public Task<TripResponseDto?> RemoveTripAsync(int id);
    public Task<TripResponseDto?> DeleteTripAsync(int id);
}