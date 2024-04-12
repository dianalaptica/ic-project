using BackEnd.Aplication.DTOs;
using BackEnd.Domain.Models;

namespace BackEnd.Aplication.Services.Authentication;

public interface IAuthenticationService
{
    Task<User?> RegisterUser(UserRegisterDto request);
    Task<AuthenticationResponseDto> LoginUser(UserLoginDto request);
    Task<AuthenticationResponseDto> RefreshToken();
}