using Microsoft.AspNetCore.Mvc;
using TableBooking.Api.Interfaces;
using TableBooking.Logic.Converters.RatingConverters;
using TableBooking.Logic.Interfaces;
using TableBooking.Model.Dtos.RatingDtos;
using TableBooking.Model.Models;

namespace TableBooking.Api.Services
{
    using Microsoft.AspNetCore.Http.HttpResults;

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
            var user = await _unitOfWork.UserRepository.GetByIdAsync(dto.AppUserId!.Value);
            var restaurant = await _unitOfWork.RestaurantRepository.GetByIdAsync(dto.RestaurantId);
            
            if (user == null) 
                return new NotFoundObjectResult($"User with id {dto.AppUserId.Value} not found.");
            
            if (restaurant == null) 
                return new NotFoundObjectResult($"Restaurant with id {dto.RestaurantId} not found.");
            
            if (dto.RatingStars < 1 || dto.RatingStars > 5)
            {
                return new BadRequestObjectResult("Rating must be between 1 and 5.");
            }
            
            var existingRating = await _unitOfWork.RatingRepository.GetRatingByUserIdAsync(dto.AppUserId.Value, dto.RestaurantId);

            if (existingRating != null)
            {
                return new BadRequestObjectResult("You have already submitted a review for this restaurant.");
            }
            
            var rating = new Rating
            {
                Id = Guid.NewGuid(),
                RatingStars = dto.RatingStars,
                Comment = dto.Comment ?? string.Empty,
                DateOfRating = DateTime.UtcNow,
                RestaurantId = dto.RestaurantId,
                AppUserId =  dto.AppUserId.Value
            };

            await _unitOfWork.RatingRepository.InsertAsync(rating);
            await _unitOfWork.SaveChangesAsync();
            var ratings = await _unitOfWork.RatingRepository.GetRatingsAsync(dto.RestaurantId) ;
            var enumerable = ratings.ToList();
            var numberOfRatings = enumerable.Count;

            if (numberOfRatings <= 0) return new OkObjectResult(_ratingConverter.RatingToRatingDto(rating));
            
            var averageRating = enumerable.Select(x => x.RatingStars).Average();
            var roundedRating = Math.Round(averageRating, 0, MidpointRounding.AwayFromZero);


            restaurant.Rating = (int)roundedRating;
            await _unitOfWork.RestaurantRepository.Update(restaurant);
            await _unitOfWork.SaveChangesAsync();

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
