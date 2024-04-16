namespace BackEnd.Aplication.DTOs;

public class TripQueryResponseDto<T>
{
    public TripQueryResponseDto(List<T> trips, int page, int pageSize, int totalCount)
    {
        Trips = trips;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public List<T> Trips { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;
}