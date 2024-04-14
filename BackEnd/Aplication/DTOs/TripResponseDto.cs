namespace BackEnd.Aplication.DTOs;

public class TripResponseDto
{
    public int Id { get; set; }
    public int GuideID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Adress { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MaxTourists { get; set; }

    public string CityName { get; set; }
}