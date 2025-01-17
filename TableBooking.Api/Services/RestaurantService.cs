using Microsoft.AspNetCore.Mvc;
using TableBooking.Model.Dtos.RestaurantDtos;
using TableBooking.Logic.Interfaces;
using TableBooking.Model.Models;
using TableBooking.Api.Interfaces;

namespace TableBooking.Api.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IUnitOfWork _unitOfWork;
    public RestaurantService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> GetRestaurantByTableIdAsync(Guid tableId)
    {
        var restaurant = await _unitOfWork.RestaurantRepository.GetRestaurantByTableIdAsync(tableId);
        
        return new OkObjectResult(restaurant);
    }

    public async Task<IActionResult> CreateRestaurantAsync(RestaurantShortInfoDto dto)
    {
        if (string.IsNullOrEmpty(dto.Name))
            return new BadRequestObjectResult("Name for new restaurant not specified.");
        
        if (string.IsNullOrEmpty(dto.Phone))
            return new BadRequestObjectResult("Phone for new restaurant not specified.");
        
        if (string.IsNullOrEmpty(dto.Location))
            return new BadRequestObjectResult("Location for new restaurant not specified.");
        
        if (string.IsNullOrEmpty(dto.Type))
            return new BadRequestObjectResult("Type for new restaurant not specified.");
        
        if (string.IsNullOrEmpty(dto.Description))
            return new BadRequestObjectResult("Description for new restaurant not specified.");
        
        if (dto.Price == null)
            return new BadRequestObjectResult("Price for new restaurant not specified.");
        
        if (dto.OpenTime == null)
            return new BadRequestObjectResult("OpenTime for new restaurant not specified.");
        
        if (dto.CloseTime == null)
            return new BadRequestObjectResult("CloseTime for new restaurant not specified.");
        
        var restaurant = new Restaurant
        {
            Name = dto.Name,
            CloseTime = dto.CloseTime,
            Description = dto.Description,
            Phone = dto.Phone,
            Location = dto.Location,
            Rating = 1,
            Price = dto.Price,
            OpenTime = dto.OpenTime,
            Type = dto.Type,
            PrimaryImageURL = dto.PrimaryImageURL,
            SecondaryImageURL = dto.SecondaryImageURL
        };
        
        await _unitOfWork.RestaurantRepository.InsertAsync(restaurant);
        await _unitOfWork.SaveChangesAsync();
        return new OkObjectResult(restaurant);
    }

    public async Task<IActionResult> DeleteRestaurantAsync(Guid restaurantId)
    {
        var restaurantToDelete = await _unitOfWork.RestaurantRepository.GetByIdAsync(restaurantId);
        if (restaurantToDelete == null)
            return new NotFoundObjectResult($"Restaurant with Id = {restaurantId} not found");
        await _unitOfWork.RestaurantRepository.Delete(restaurantToDelete.Id);
        await _unitOfWork.SaveChangesAsync();
        return new OkObjectResult(restaurantToDelete);
    }

    public async Task<IActionResult> GetAllRestaurantsAsync(string? restaurantName, Price? price)
    {
        var restaurants = await _unitOfWork.RestaurantRepository.GetRestaurantsAsync(restaurantName, price);
        foreach (var restaurant in restaurants)
        {
            var tables = await _unitOfWork.TableRepository.GetTablesByRestaurantIdAsync(restaurant.Id);
            restaurant.Tables = tables ?? new [] { new Table { RestaurantId = restaurant.Id }};;
        }
        if (restaurants == null) return new BadRequestObjectResult("No restaurants found");
        return new OkObjectResult(restaurants);
    }

    public async Task<IActionResult> GetRestaurantByIdAsync(Guid restaurantId)
    {
        var restaurant = await _unitOfWork.RestaurantRepository.GetByIdAsync(restaurantId);
        var tables = await _unitOfWork.TableRepository.GetTablesByRestaurantIdAsync(restaurantId);
        if (restaurant == null)
            return new NotFoundObjectResult($"Restaurant with Id = {restaurantId} not found");
        restaurant.Tables = tables ?? new [] { new Table { RestaurantId = restaurantId }};
        return new OkObjectResult(restaurant);
    }

    public async Task<IActionResult> UpdateRestaurantAsync(RestaurantShortInfoDto dto, Guid restaurantId)
    {
        var restaurant = await _unitOfWork.RestaurantRepository.GetByIdAsync(restaurantId);
        if (restaurant == null)
            return new BadRequestObjectResult($"Booking with id {restaurantId} doesn't exist.");

        var newRestaurant = new Restaurant
        {
            Id = restaurant.Id,
            Description = dto.Description,
            Location = dto.Location ?? restaurant.Location,
            Name = dto.Name ?? restaurant.Name,
            Phone = dto.Phone ?? restaurant.Phone,
            Price = dto.Price,
            PrimaryImageURL = dto.PrimaryImageURL,
            SecondaryImageURL = dto.SecondaryImageURL,
            Tables = restaurant.Tables,
            Type = dto.Type ?? restaurant.Type,
            Rating = restaurant.Rating,
            CloseTime = dto.CloseTime,
            OpenTime = dto.OpenTime
        };

        await _unitOfWork.RestaurantRepository.Update(newRestaurant);
        await _unitOfWork.SaveChangesAsync();

        return new OkObjectResult(newRestaurant);
    }
}