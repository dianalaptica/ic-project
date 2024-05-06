namespace BackEnd.Aplication.DTOs;

public class NotificationResponseDto
{
    public int Id { get; set; }
    public int TripId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}