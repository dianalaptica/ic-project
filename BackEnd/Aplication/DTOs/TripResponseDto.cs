namespace BackEnd.Aplication.DTOs;

public class TripResponseDto
{
    public Guid Id { get; set; }
    public Guid TouristGuide { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MaxTourists { get; set; }
    public List<Guid>? Tourists { get; set; }
}