using BackEnd.Aplication.DTOs;
using BackEnd.Aplication.Services.Notification;
using BackEnd.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Web.Controllers;

[Route("api/notification")]
[ApiController]
[Authorize]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost]
    [Authorize(Roles = "Guide")]
    public async Task<ActionResult<TripNotification>> CreateNotification([FromBody] NotificationCreateDto notification)
    {
        try
        {
            var result = await _notificationService.CreateNotificationAsync(notification);
            if (result is not null)
            {
                return CreatedAtAction(nameof(GetNotification), new { id = result.Id }, result);
            }
            return BadRequest();
        } catch
        {
            return StatusCode(500, "Error connecting to Database.");
        }
        
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Guide, Tourist")]
    public async Task<ActionResult<TripNotification>> GetNotification(int id)
    {
        try
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id, false);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }
        catch
        {
            return StatusCode(500, "Error connecting to Database.");
        }
    }
    
    [HttpGet("user")]
    [Authorize(Roles = "Tourist")]
    public async Task<ActionResult<UserNotificationResponseDto>> GetUsersNotification()
    {
        try
        {
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(false);
            if (notifications == null || !notifications.Any())
            {
                return NotFound();
            }
            return Ok(notifications);
        }
        catch
        {
            return StatusCode(500, "Error connecting to Database.");
        }
    }
    
    [HttpPatch("user/{id}")]
    [Authorize(Roles = "Tourist")]
    public async Task<ActionResult<TripNotification>> UpdateReadStatus([FromRoute]int id)
    {
        try
        {
            var notification = await _notificationService.UpdateReadStatusAsync(id, true);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }
        catch
        {
            return StatusCode(500, "Error connecting to Database.");
        }
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Guide")]
    public async Task<ActionResult<TripNotification>> DeleteNotification([FromRoute]int id)
    {
        try
        {
            var notification = await _notificationService.DeleteNotificationAsync(id);
            if (notification == null)
            {
                return BadRequest();
            }

            return NoContent();
        }
        catch
        {
            return StatusCode(500, "Error connecting to Database.");
        }
    }
    
    // TODO: THERE MIGHT BE NICE TO ADD MORE VALIDATION VIA JWT TOKEN
    // TODO: ONLY A GUIDE CAN MODIFY HIS TRIPS NOT ANOTHER GUIDE
}