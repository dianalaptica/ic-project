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

    public CityController(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
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
}