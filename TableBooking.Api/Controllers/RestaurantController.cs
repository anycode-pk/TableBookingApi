namespace TableBooking.Api.Controllers;

using Interfaces;
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
    public async Task<IActionResult> GetRestaurants([FromQuery]string? restaurantName, [FromQuery]Price? price)
    {
        return await _restaurantService.GetAllRestaurantsAsync(restaurantName, price);
    }

    [HttpGet("GetRestaurantById/{id}")]
    public async Task<IActionResult> GetRestaurantById(Guid id)
    {
        return await _restaurantService.GetRestaurantByIdAsync(id);
    }
    
    [HttpGet("GetRestaurantByTableId/{tableId}")]
    public async Task<IActionResult> GetRestaurantByTableId(Guid tableId)
    {
        return await _restaurantService.GetRestaurantByTableIdAsync(tableId);
    }

    [HttpPost("CreateRestaurant")]
    public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantShortInfoDto restaurantShortInfoDto)
    {
        return await _restaurantService.CreateRestaurantAsync(restaurantShortInfoDto);
    }

    [HttpDelete("DeleteRestaurant/{id:Guid}")]
    public async Task<IActionResult> DeleteRestaurant(Guid id)
    {
        return await _restaurantService.DeleteRestaurantAsync(id);
    }

    [HttpPut("UpdateRestaurant/{restaurantId}")]
    public async Task<IActionResult> UpdateRestaurant([FromBody] RestaurantShortInfoDto restaurantShortInfoDto, Guid restaurantId)
    {
        return await _restaurantService.UpdateRestaurantAsync(restaurantShortInfoDto, restaurantId);
    }
}