using Microsoft.AspNetCore.Mvc;
using TableBooking.Model.Dtos.RestaurantDtos;
using TableBooking.Model.Models;

namespace TableBooking.Api.Interfaces
{
    public interface IRestaurantService
    {
        public Task<IActionResult> GetAllRestaurantsAsync(string? restaurantName, Price? price);
        public Task<IActionResult> GetRestaurantByIdAsync(Guid restaurantId);
        public Task<IActionResult> CreateRestaurantAsync(RestaurantShortInfoDto dto);
        public Task<IActionResult> UpdateRestaurantAsync(RestaurantShortInfoDto dto, Guid restaurantId);
        public Task<IActionResult> DeleteRestaurantAsync(Guid restaurantId);
    }

}
