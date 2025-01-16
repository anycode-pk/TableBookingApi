using Microsoft.AspNetCore.Mvc;
using TableBooking.Model.Dtos.RestaurantDtos;
using TableBooking.Logic.Interfaces;
using TableBooking.Model.Models;
using TableBooking.Api.Interfaces;
using TableBooking.Model.Dtos.BookingDtos;

namespace TableBooking.Api.Services
{

    public class RestaurantService : IRestaurantService
    {
        public IUnitOfWork _unitOfWork;
        public RestaurantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> CreateRestaurantAsync(RestaurantShortInfoDto dto)
        {
            var restaurant = new Restaurant
            {
                Name = dto.Name,
                CloseTime = dto.CloseTime,
                Description = dto.Description,
                Phone = dto.Phone,
                Location = dto.Location,
                Rating = 0,
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
            if (restaurants == null) return new BadRequestObjectResult("No restaurants found");
            return new OkObjectResult(restaurants);
        }

        public async Task<IActionResult> GetRestaurantByIdAsync(Guid restaurantId)
        {
            var restaurant = await _unitOfWork.RestaurantRepository.GetByIdAsync(restaurantId);
            if (restaurant == null)
                return new NotFoundObjectResult($"Restaurant with Id = {restaurantId} not found");
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
                Location = dto.Location,
                Name = dto.Name,
                Phone = dto.Phone,
                Price = dto.Price,
                PrimaryImageURL = dto.PrimaryImageURL,
                SecondaryImageURL = dto.SecondaryImageURL,
                Tables = restaurant.Tables,
                Type = dto.Type,
                Rating = restaurant.Rating,
                CloseTime = dto.CloseTime,
                OpenTime = dto.OpenTime
            };

            await _unitOfWork.RestaurantRepository.Update(newRestaurant);
            await _unitOfWork.SaveChangesAsync();

            return new OkObjectResult(newRestaurant);
        }
    }
}
