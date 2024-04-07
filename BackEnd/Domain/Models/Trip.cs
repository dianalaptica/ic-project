namespace BackEnd.Domain.Models;

public class Trip
{
    public required Guid Id { get; set; }
    public required User TouristGuide { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Location { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required int MaxTourists { get; set; }
    public List<User>? Tourists { get; set; }
}