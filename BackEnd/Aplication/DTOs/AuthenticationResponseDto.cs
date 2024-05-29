namespace BackEnd.Aplication.DTOs;

public class AuthenticationResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime TokenCreated { get; set; }
    public DateTime TokenExpires { get; set; }
    public string Role { get; set; } = string.Empty;
}