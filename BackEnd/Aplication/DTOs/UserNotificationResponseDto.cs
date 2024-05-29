using System.Globalization;

namespace BackEnd.Aplication.DTOs;

public class UserNotificationResponseDto
{
    public int NotificationId { get; set; }
    public int TripId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public string TripTitle {  get; set; }
}