namespace BackEnd.Aplication.DTOs;

public class NotificationCreateDto
{
    public int TripId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}