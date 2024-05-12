namespace BackEnd.Aplication.DTOs;

public class TripCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Adress { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MaxTourists { get; set; }
    public int CityId { get; set; }
    public IFormFile Image { get; set; }
}