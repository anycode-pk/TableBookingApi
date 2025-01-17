namespace TableBooking.Api.Controllers;

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

    public RestaurantController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet("GetAllRestaurants")]
    [AllowAnonymous]
    public async Task<IActionResult> GetRestaurants([FromQuery]string? restaurantName, [FromQuery]Price? price)
    {
        return await _restaurantService.GetAllRestaurantsAsync(restaurantName, price);
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

    [HttpPost("CreateRestaurant")]
    [Authorize]
    public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantShortInfoDto restaurantShortInfoDto)
    {
        return await _restaurantService.CreateRestaurantAsync(restaurantShortInfoDto);
    }

    [HttpDelete("DeleteRestaurant/{restaurantId:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteRestaurant(Guid restaurantId)
    {
        return await _restaurantService.DeleteRestaurantAsync(restaurantId);
    }

    [HttpPut("UpdateRestaurant/{restaurantId:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateRestaurant([FromBody] RestaurantShortInfoDto restaurantShortInfoDto, Guid restaurantId)
    {
        return await _restaurantService.UpdateRestaurantAsync(restaurantShortInfoDto, restaurantId);
    }
}