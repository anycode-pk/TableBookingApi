using Microsoft.AspNetCore.Mvc;
using TableBooking.Model.Dtos.RatingDtos;

namespace TableBooking.Api.Interfaces
{
    public interface IRatingService
    {
        public Task<IActionResult> GetAllRatingsAsync(Guid restaurantId);
        public Task<IActionResult> GetRatingByIdAsync(Guid ratingId);
        public Task<IActionResult> CreateRatingAsync(CreateRatingDto dto);
        public Task<IActionResult> DeleteRatingAsync(Guid ratingId);
    }
}
