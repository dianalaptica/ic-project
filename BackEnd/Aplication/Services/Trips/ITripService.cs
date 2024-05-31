using System.Linq.Expressions;
using BackEnd.Aplication.DTOs;
using BackEnd.Domain.Models;

namespace BackEnd.Aplication.Services.Trips;

public interface ITripService
{
    public Task<IEnumerable<TripResponseDto>> GetAllTripsAsync(bool trackChanges);
    public Task<TripResponseDto?> GetTripByIdAsync(int id, bool trackChanges);
    public Task<TripQueryResponseDto<TripResponseDto>> GetTripsByQuery(int? cityId,
        string? searchTitle,
        string? sortColumn,
        string? sortOrder,
        bool hasJoined,
        bool isUpcoming,
        int page,
        int pageSize,
        bool trackChanges);
    public Task<TripResponseDto?> CreateTripAsync(TripCreateDto tripCreateDto, byte[] image);
    public Task<TripStatsDto> GetTouristTripStats(bool trackChanges);
    public Task<TripStatsDto> GetGuideTripStats(bool trackChanges);
    public Task<TripResponseDto?> JoinTripAsync(int id);
    public Task<TripResponseDto?> RemoveTripAsync(int id);
    public Task<TripResponseDto?> DeleteTripAsync(int id);
}