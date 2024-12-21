using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TableBooking.Api.Interfaces;
using TableBooking.Model.Dtos.RestaurantDtos;
using TableBooking.Model.Models;

namespace TableBooking.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("GetAllRestaurants")]
        public async Task<IActionResult> GetRestaurants([FromQuery]string? restuarantName, [FromQuery]Price? price)
        {
            return await _restaurantService.GetAllRestaurantsAsync(restuarantName, price);
        }

        [HttpGet("GetRestaurantById/{id}")]
        public async Task<IActionResult> GetRestaurantById(Guid id)
        {
            return await _restaurantService.GetRestaurantByIdAsync(id);
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
}
