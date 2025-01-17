namespace TableBooking.Api.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Model.Dtos.RestaurantDtos;
using Model.Models;

public interface IRestaurantService
{
    public Task<IActionResult> GetAllRestaurantsAsync(string? restaurantName, Price? price);
    public Task<IActionResult> GetRestaurantByIdAsync(Guid restaurantId);
    public Task<IActionResult> GetRestaurantByTableIdAsync(Guid tableId);
    public Task<IActionResult> CreateRestaurantAsync(RestaurantShortInfoDto dto);
    public Task<IActionResult> UpdateRestaurantAsync(RestaurantShortInfoDto dto, Guid restaurantId);
    public Task<IActionResult> DeleteRestaurantAsync(Guid restaurantId);
}