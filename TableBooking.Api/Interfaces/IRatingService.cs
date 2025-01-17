namespace TableBooking.Api.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Model.Dtos.RatingDtos;

public interface IRatingService
{
    public Task<IActionResult> GetAllRatingsAsync(Guid restaurantId);
    public Task<IActionResult> GetRatingByIdAsync(Guid ratingId);
    public Task<IActionResult> CreateRatingAsync(CreateRatingDto dto);
    public Task<IActionResult> DeleteRatingAsync(Guid ratingId);
}