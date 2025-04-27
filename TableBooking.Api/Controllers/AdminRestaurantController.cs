namespace TableBooking.Api.Controllers;

using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.BookingDtos;
using Model.Dtos.RestaurantDtos;
using System.Security.Claims;

[Route("[controller]")]
[ApiController]
[Authorize(Roles = "Restaurant")]
public class AdminRestaurantController : ControllerBase
{
    private readonly IBookingService _bookingService;
    private readonly IRestaurantService _restaurantService;

    public AdminRestaurantController(IBookingService bookingService, IRestaurantService restaurantService)
    {
        _bookingService = bookingService;
        _restaurantService = restaurantService;
    }
    
    [HttpGet("Bookings")]
    [ProducesResponseType(typeof(List<BookingDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Restaurant")]
    public async Task<IActionResult> GetRestaurantBookingsByRestaurantId([FromQuery] Guid restaurantId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
    {
        return await _bookingService.GetBookingsForRestaurantById(restaurantId, fromDate, toDate);
    }
    
    [HttpPost("CreateRestaurant")]
    public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantShortInfoDto restaurantShortInfoDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User id is required when trying to create a restaurant. Can't find ID for currently logged user");
        }
        
        restaurantShortInfoDto.OwnerId = userId;
        
        return await _restaurantService.CreateRestaurantAsync(restaurantShortInfoDto);
    }
    
    [HttpDelete("DeleteRestaurant/{restaurantId:guid}")]
    public async Task<IActionResult> DeleteRestaurant(Guid restaurantId)
    {
        return await _restaurantService.DeleteRestaurantAsync(restaurantId);
    }

    [HttpPut("UpdateRestaurant/{restaurantId:guid}")]
    public async Task<IActionResult> UpdateRestaurant([FromBody] RestaurantShortInfoDto restaurantShortInfoDto, Guid restaurantId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User id is required when trying to update a restaurant. Can't find ID for currently logged user");
        }
        
        restaurantShortInfoDto.OwnerId = userId;
        
        return await _restaurantService.UpdateRestaurantAsync(restaurantShortInfoDto, restaurantId);
    }
}