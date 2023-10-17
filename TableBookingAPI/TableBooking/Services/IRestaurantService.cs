﻿using Microsoft.AspNetCore.Mvc;
using TableBooking.DTOs;
using TableBooking.Logic.Interfaces;
using TableBooking.Model;
using Microsoft.AspNetCore.Mvc;
using TableBooking.Api.Services;
using TableBooking.DTOs;
using TableBooking.Logic.Interfaces;
using TableBooking.Model;

namespace TableBooking.Api.Services
{
    public interface IRestaurantService
    {
        public Task<IActionResult> GetAllRestaurantsAsync();
        public Task<IActionResult> GetRestaurantByIdAsync(Guid restaurantId);
        public Task<IActionResult> CreateRestaurantAsync(RestaurantShortInfoDTO dto);
        public Task<IActionResult> UpdateRestaurantAsync(RestaurantShortInfoDTO dto);
        public Task<IActionResult> DeleteRestaurantAsync(Guid restaurantId);
    }

    public class RestaurantService : IRestaurantService
    {
        public IUnitOfWork _unitOfWork;
        public RestaurantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> CreateRestaurantAsync(RestaurantShortInfoDTO dto)
        {
            var restaurant = new Restaurant
            {
                Name = dto.Name,
                CloseTime = dto.CloseTime,
                Description = dto.Description,
                Location = dto.Location,
                Rating = 0,
                Price = dto.Price,
                OpenTime = dto.OpenTime,
                Type = dto.Type
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
            await _unitOfWork.RestaurantRepository.Delete(restaurantToDelete);
            await _unitOfWork.SaveChangesAsync();
            return new OkObjectResult(restaurantToDelete);
        }

        public async Task<IActionResult> GetAllRestaurantsAsync()
        {
            var restaurants = await _unitOfWork.RestaurantRepository.GetRestaurantsAsync();
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

        public Task<IActionResult> UpdateRestaurantAsync(RestaurantShortInfoDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
