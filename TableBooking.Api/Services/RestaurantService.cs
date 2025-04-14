namespace TableBooking.Api.Services;

using Interfaces;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.RestaurantDtos;
using Model.Models;

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
        var restaurant = new Restaurant
        {
            Name = dto.Name!,
            Description = dto.Description!,
            Phone = dto.Phone!,
            Location = dto.Location!,
            Rating = 1,
            Price = dto.Price,
            OpeningAndClosingHours = dto.OpeningAndClosingHours,
            Type = dto.Type!,
            PrimaryImageUrl = dto.PrimaryImageURL,
            SecondaryImageUrl = dto.SecondaryImageURL
        };

        await _unitOfWork.RestaurantRepository.InsertAsync(restaurant);
        await _unitOfWork.SaveChangesAsync();
        return new OkObjectResult(restaurant);
    }

    public async Task<IActionResult> DeleteRestaurantAsync(Guid restaurantId)
    {
        var restaurantToDelete = await _unitOfWork.RestaurantRepository.GetByIdAsync(restaurantId);

        if (restaurantToDelete == null)
            return new NotFoundObjectResult(new { message = $"Restaurant with id {restaurantId} not found." });

        await _unitOfWork.RestaurantRepository.Delete(restaurantToDelete.Id);
        await _unitOfWork.SaveChangesAsync();

        return new OkObjectResult(restaurantToDelete);
    }

    public async Task<IActionResult> FavouriteRestaurantAsync(Guid userId, Guid restaurantId)
    {
        var restaurantToBeFavourite = await _unitOfWork.RestaurantRepository.GetByIdAsync(restaurantId);
    
        if (restaurantToBeFavourite == null)
            return new NotFoundObjectResult(new { message = $"Restaurant with id {restaurantId} not found." });
    
        var user = await _unitOfWork.UserRepository.GetUserById(userId);
        
        var userFavs = await _unitOfWork.UserRepository.GetFavouriteRestaurantsByUserId(userId);

        var existingFavourite = userFavs
            .FirstOrDefault(uf => uf.RestaurantId == restaurantId);

        if (existingFavourite != null)
        {
            user.FavouriteRestaurants.Remove(existingFavourite);
        }
        else
        {
            user.FavouriteRestaurants.Add(new UserFavouriteRestaurant
            {
                UserId = userId,
                RestaurantId = restaurantId
            });
        }
    
        await _unitOfWork.SaveChangesAsync();
    
        return new OkObjectResult(user.ToDto());    
    }


    public async Task<RestaurantSearchResponseDto> SearchRestaurantsAsync(string? restaurantName, Price? price,
        bool? searchForEmptyTablesOnly, DateTime? requestedDateTimeForEmptyTables,
        int? numberOfPeopleForEmptyTables, int page, int pageSize)
    {
        var responseDto = await _unitOfWork.RestaurantRepository
            .SearchRestaurantsAsync(restaurantName, price, searchForEmptyTablesOnly, requestedDateTimeForEmptyTables,
                numberOfPeopleForEmptyTables, page, pageSize);
        
        return new RestaurantSearchResponseDto
        {
            Restaurants = responseDto.Restaurants,
            Suggestions = responseDto.Suggestions
        };
    }

    public async Task<IActionResult> GetRestaurantByIdAsync(Guid restaurantId)
    {
        var restaurant = await _unitOfWork.RestaurantRepository.GetByIdAsync(restaurantId);

        if (restaurant == null)
            return new NotFoundObjectResult(new { message = $"Restaurant with id {restaurantId} not found." });
        
        var tables = await _unitOfWork.TableRepository.GetTablesByRestaurantIdAsync(restaurantId);
        restaurant.Tables = tables;

        return new OkObjectResult(restaurant);
    }

    public async Task<IActionResult> UpdateRestaurantAsync(RestaurantShortInfoDto dto, Guid restaurantId)
    {
        var restaurant = await _unitOfWork.RestaurantRepository.GetByIdAsync(restaurantId);

        if (restaurant == null)
            return new NotFoundObjectResult(new { message = $"Restaurant with id {restaurantId} not found." });

        var newRestaurant = new Restaurant
        {
            Id = restaurant.Id,
            Description = dto.Description ?? restaurant.Description,
            Location = dto.Location ?? restaurant.Location,
            Name = dto.Name ?? restaurant.Name,
            Phone = dto.Phone ?? restaurant.Phone,
            Price = dto.Price,
            PrimaryImageUrl = dto.PrimaryImageURL,
            SecondaryImageUrl = dto.SecondaryImageURL,
            Tables = restaurant.Tables,
            Type = dto.Type ?? restaurant.Type,
            Rating = restaurant.Rating,
            OpeningAndClosingHours = dto.OpeningAndClosingHours
        };

        await _unitOfWork.RestaurantRepository.Update(newRestaurant);
        await _unitOfWork.SaveChangesAsync();

        return new OkObjectResult(newRestaurant);
    }
}