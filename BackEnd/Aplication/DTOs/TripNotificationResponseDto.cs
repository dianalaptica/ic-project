namespace BackEnd.Aplication.DTOs;

public class TripNotificationResponseDto
{
    public int Id { get; set; }
    public int TripId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string TripTitle {  get; set; }
}