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

    public async Task<User> RegisterUser(UserRegisterDto request)
    {
        CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = "user" // TODO: make it nicer
        };
        
        _userRepository.CreateUser(user);
        await _userRepository.SaveAsync();

        return user;
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
        SetRefreshToken(user, refreshToken);

        return new AuthenticationResponseDto
        {
            Success = true,
            Message = "Login successful!",
            Token = jwt,
            RefreshToken = refreshToken.Token,
            TokenExpires = refreshToken.Expires,
            TokenCreated = refreshToken.Created,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<AuthenticationResponseDto> RefreshToken()
    {
        var refreshToken = _httpContext?.HttpContext?.Request.Cookies["refreshToken"];
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
            Role = user.Role
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
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:key").Value));
        var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddHours(1), signingCredentials: sc);
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
    
    private RefreshToken CreateRefreshToken()
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddDays(7),
            Created = DateTime.Now
        };

        return refreshToken;
    }

    private async void SetRefreshToken(User user, RefreshToken refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.Expires
        };
        _httpContext?.HttpContext?.Response
            .Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

        user.RefreshToken = refreshToken.Token;
        user.TokenCreated = refreshToken.Created;
        user.TokenExpires = refreshToken.Expires;
        
        await _userRepository.SaveAsync();// TODO: Might not be compelte
    }
}