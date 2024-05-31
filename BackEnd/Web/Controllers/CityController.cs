using System.Security.Claims;
using BackEnd.Aplication.DTOs;
using BackEnd.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Web.Controllers;

[Route("api/city")]
[ApiController]
[Authorize]
public class CityController : ControllerBase
{
    private readonly ICityRepository _cityRepository;
    private readonly IAppliedForGuideRepository _appliedForGuideRepository;
    private readonly IHttpContextAccessor _httpContext;

    public CityController(ICityRepository cityRepository, IAppliedForGuideRepository appliedForGuideRepository, IHttpContextAccessor httpContext)
    {
        _cityRepository = cityRepository;
        _appliedForGuideRepository = appliedForGuideRepository;
        _httpContext = httpContext;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CityResponseDto>>> GetCities()
    {
        try
        {
            var cities = await _cityRepository.GetAllCitiesWithCountriesAsync(false);
            if (!cities.IsNullOrEmpty())
            {
                return Ok(cities.Select(c => new CityResponseDto
                {
                    Id = c.Id,
                    CityName = c.Name,
                    CountryName = c.Country.Name
                }));
            }
            return NotFound();
        } catch
        {
            return StatusCode(500, "Error connecting to Database.");
        }
    }
    
    [HttpGet("guide")]
    [Authorize(Roles = "Guide")]
    public async Task<ActionResult<CityResponseDto>> GetGuideCity()
    {
        try
        {
            var guideId = int.Parse(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var city = await _appliedForGuideRepository.GetByIdWithCityAsync(guideId,false);
            if (city is not null)
            {
                return Ok(city);
            }
            return NotFound();
        } catch
        {
            return StatusCode(500, "Error connecting to Database.");
        }
    }
}