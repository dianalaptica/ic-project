using System.Net.Mime;
using BackEnd.Aplication.DTOs;
using BackEnd.Aplication.Services.Authentication;
using BackEnd.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Web.Controllers;

[Route("api/auth")]
[ApiController]
[Authorize]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authService;
    
    public AuthenticationController(IAuthenticationService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthenticationResponseDto>> RegisterUser([FromBody] UserRegisterDto request)
    {
        var response = await _authService.RegisterUser(request);
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest();
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthenticationResponseDto>> LoginUser([FromBody] UserLoginDto request)
    {
        var response = await _authService.LoginUser(request);
        if(response.Success)
            return Ok(response);
        return Unauthorized(new {message = "Credentials are not valid!"});
    }

    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<string>> RefreshToken([FromBody] string refreshToken)
    {
        Console.WriteLine("Refresh token"); 
        var response = await _authService.RefreshToken(refreshToken);
        if (response.Success)
        {
            return Ok(response);
        }
        return Unauthorized(response);
    }
}