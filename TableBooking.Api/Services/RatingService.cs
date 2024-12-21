using Microsoft.AspNetCore.Mvc;
using TableBooking.Api.Interfaces;
using TableBooking.Logic.Converters.RatingConverters;
using TableBooking.Logic.Interfaces;
using TableBooking.Model.Dtos.RatingDtos;
using TableBooking.Model.Models;

namespace TableBooking.Api.Services
{
    public class RatingService : IRatingService
    {
        private IUnitOfWork _unitOfWork;
        private IRatingConverter _ratingConverter;

        public RatingService(IUnitOfWork unitOfWork, IRatingConverter ratingConverter)
        { 
            _unitOfWork = unitOfWork;
            _ratingConverter = ratingConverter;
        }
        public async Task<IActionResult> CreateRatingAsync(CreateRatingDto dto)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(dto.AppUserId);
            var restaurant = await _unitOfWork.RestaurantRepository.GetByIdAsync(dto.RestaurantId);
            if (user == null || restaurant == null) return new BadRequestObjectResult(string.Empty);

            var rating = new Rating
            {
                RatingStars = dto.RatingStars,
                Comment = dto.Comment,
                DateOfRating = dto.DateOfRating,
                RestaurantId = dto.RestaurantId,
                AppUserId = dto.AppUserId
            };

            await _unitOfWork.RatingRepository.InsertAsync(rating);
            await _unitOfWork.SaveChangesAsync();
            var ratings = await _unitOfWork.RatingRepository.GetRatingsAsync(dto.RestaurantId) ;
            var numberOfRaitings = ratings.Count();
            var result = 0d;

            if (numberOfRaitings > 0 && numberOfRaitings % 5 == 0)
            {
                result = ratings.Select(x => x.RatingStars).Average();
                restaurant.Rating = result;
                await _unitOfWork.RestaurantRepository.Update(restaurant);
                await _unitOfWork.SaveChangesAsync();
            }

            return new OkObjectResult(_ratingConverter.RatingToRatingDto(rating));
        }

        public async Task<IActionResult> DeleteRatingAsync(Guid ratingId)
        {
            var ratingToDelete = await _unitOfWork.RatingRepository.GetRating(ratingId);
            if (ratingToDelete == null)
                return new NotFoundObjectResult($"Rating with Id = {ratingId} not found");
            var deletedRating = _ratingConverter.RatingToRatingDto(ratingToDelete);
            await _unitOfWork.RatingRepository.Delete(ratingId);
            await _unitOfWork.SaveChangesAsync();
            return new OkObjectResult(deletedRating);
        }

        public async Task<IActionResult> GetAllRatingsAsync(Guid restaurantId)
        {
            var ratings = await _unitOfWork.RatingRepository.GetRatingsAsync(restaurantId);
            if (ratings == null) return new BadRequestObjectResult("No ratings found");
            var ratingDtos = _ratingConverter.RatingsToRatingDtos(ratings);
            return new OkObjectResult(ratingDtos);
        }

        public async Task<IActionResult> GetRatingByIdAsync(Guid ratingId)
        {
            var rating = await _unitOfWork.RatingRepository.GetByIdAsync(ratingId);
            if (rating == null)
                return new NotFoundObjectResult($"Rating with Id = {ratingId} not found");
            return new OkObjectResult(rating);
        }
    }
}
