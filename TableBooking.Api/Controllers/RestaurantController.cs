namespace TableBooking.Api.Controllers;

using System.Security.Claims;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.RestaurantDtos;
using Model.Models;

[Route("[controller]")]
[ApiController]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantController(IRestaurantService restaurantService, IUserService userService)
    {
        _restaurantService = restaurantService;
    }
    
    [HttpGet("search")]
    [AllowAnonymous]
    public async Task<IActionResult> GetRestaurants([FromQuery] string? restaurantName, [FromQuery] Price? price,
        [FromQuery] bool? searchForEmptyTablesOnly, [FromQuery] DateTime? requestedDateTimeForEmptyTables,
        [FromQuery] int? numberOfPeopleForEmptyTables,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _restaurantService
            .SearchRestaurantsAsync(restaurantName, price, searchForEmptyTablesOnly, 
                requestedDateTimeForEmptyTables, numberOfPeopleForEmptyTables,
                page, pageSize);
        
        return Ok(result);
    }
    
    [HttpGet("GetRestaurantById/{restaurantId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetRestaurantById(Guid restaurantId)
    {
        return await _restaurantService.GetRestaurantByIdAsync(restaurantId);
    }

    [HttpGet("GetRestaurantByTableId/{tableId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetRestaurantByTableId(Guid tableId)
    {
        return await _restaurantService.GetRestaurantByTableIdAsync(tableId);
    }
    
    [HttpPost("Favourite/{restaurantId:guid}")]
    [Authorize]
    public async Task<IActionResult> FavouriteRestaurant(Guid restaurantId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) throw new UnauthorizedAccessException("User is not authenticated.");
        
        return await _restaurantService.FavouriteRestaurantAsync(Guid.Parse(userId), restaurantId);
    }
}