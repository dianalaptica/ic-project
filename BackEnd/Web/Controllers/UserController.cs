using System.Net.Mime;
using System.Security.Claims;
using BackEnd.Aplication.DTOs;
using BackEnd.Domain.Interfaces;
using BackEnd.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Web.Controllers;

[Route("api/user")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IAppliedForGuideRepository _appliedForGuideRepository;
    private readonly IHttpContextAccessor _httpContext;
    
    public UserController(IUserRepository userRepository, IHttpContextAccessor httpContext, IAppliedForGuideRepository appliedForGuideRepository)
    {
        _userRepository = userRepository;
        _httpContext = httpContext;
        _appliedForGuideRepository = appliedForGuideRepository;
    }
    
    // TODO: change response to be nicer
    [HttpPost("apply-guide")]
    [Authorize(Roles = "Tourist")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AppliedForGuide>> ApplyGuide([FromBody] UserApplicationDto request)
    {
        var userId = int.Parse(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        if (userId == null)
        {
            return BadRequest();
        }

        var application = new AppliedForGuide
        {
            UserId = userId,
            IdentityCard = new byte[1],
            CityId = request.CityId,
            IsApproved = false
        };
        
        _appliedForGuideRepository.Create(application);
        await _appliedForGuideRepository.SaveChangesAsync();
        
        return Ok(application);
    }
    
    [HttpPatch("accept-guide/{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AppliedForGuide>> AcceptGuide([FromRoute]int id)
    {
        var application = await _appliedForGuideRepository.GetByIdAsync(id, true);

        if (application == null)
        {
            return BadRequest();
        }

        application.IsApproved = true;
        _appliedForGuideRepository.Update(application);
        await _appliedForGuideRepository.SaveChangesAsync();
        
        var user = await _userRepository.GetByIdAsync(application.UserId, true);
        if (user == null)
        {
            return BadRequest();
        }
        
        user.RoleId = 3; // the guide id
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
        
        return Ok(application);
    }
    
}