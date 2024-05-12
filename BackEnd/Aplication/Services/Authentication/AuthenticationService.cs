using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BackEnd.Aplication.DTOs;
using BackEnd.Domain.Interfaces;
using BackEnd.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Aplication.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContext;

    public AuthenticationService(IUserRepository userRepository, IConfiguration configuration, IHttpContextAccessor httpContext)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _httpContext = httpContext;
    }

    public async Task<AuthenticationResponseDto?> RegisterUser(UserRegisterDto request)
    {
        if (await _userRepository.GetUserByEmail(request.Email, false) != null) return null;
        if (await _userRepository.GetUserByPhoneNumber(request.PhoneNumber, false) != null) return null;
        CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);
        
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Gender = request.Gender ?? "Not specified",
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            RoleId = 2, // the tourist id
            TokenExpires = DateTime.Today,
            TokenCreated = DateTime.Today,
            RefreshToken = string.Empty
            // TODO: make a utils class with enum of roles
        };
        
        _userRepository.Create(user);
        await _userRepository.SaveChangesAsync();
        
        return new AuthenticationResponseDto
        {
            Success = true,
            Message = "Registered successfully!",
            Email = user.Email
        };
    }

    public async Task<AuthenticationResponseDto> LoginUser(UserLoginDto request)
    {
        var user = await _userRepository.GetUserByEmail(request.Email, true);

        if (user is null)
        {
            return new AuthenticationResponseDto
            {
                Message = "User not found!",
                Success = false
            };
        }

        if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return new AuthenticationResponseDto
            {
                Message = "Wrong password for " + user.Email,
                Success = false
            };
        }

        var jwt = CreateToken(user);
        var refreshToken = CreateRefreshToken();
        await SetRefreshToken(user, refreshToken);

        return new AuthenticationResponseDto
        {
            Success = true,
            Message = "Login successful!",
            Token = jwt,
            RefreshToken = refreshToken.Token,
            TokenExpires = refreshToken.Expires,
            TokenCreated = refreshToken.Created,
            Email = user.Email,
            Role = user.Role.Name
        };
    }

    public async Task<AuthenticationResponseDto> RefreshToken(string refreshToken)
    {
        var user = await _userRepository.GetUserByRefreshToken(refreshToken, false);

        if (user == null)
            return new AuthenticationResponseDto {Message = "Invalid Token"};
        if (user.TokenExpires < DateTime.Now)
            return new AuthenticationResponseDto {Message = "Token Expired"};

        var jwt = CreateToken(user);

        return new AuthenticationResponseDto
        {
            Success = true,
            Token = jwt,
            Email = user.Email,
            Role = user.Role.Name
        };
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computeHash.SequenceEqual(passwordHash);
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role.Name)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:key").Value));
        var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddHours(5), signingCredentials: sc);
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
    
    private RefreshTokenDto CreateRefreshToken()
    {
        var refreshToken = new RefreshTokenDto
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddHours(10),
            Created = DateTime.Now
        };

        return refreshToken;
    }

    private async Task SetRefreshToken(User user, RefreshTokenDto refreshTokenDto)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshTokenDto.Expires
        };
        _httpContext?.HttpContext?.Response
            .Cookies.Append("refreshToken", refreshTokenDto.Token, cookieOptions);

        user.RefreshToken = refreshTokenDto.Token;
        user.TokenCreated = refreshTokenDto.Created;
        user.TokenExpires = refreshTokenDto.Expires;
        
        await _userRepository.SaveChangesAsync();// TODO: Might not be complete
    }
}