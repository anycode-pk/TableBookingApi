namespace TableBooking.Api.Services;

using Interfaces;
using Logic.Converters.RatingConverters;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.RatingDtos;
using Model.Models;

public class RatingService : IRatingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRatingConverter _ratingConverter;

    public RatingService(IUnitOfWork unitOfWork, IRatingConverter ratingConverter)
    { 
        _unitOfWork = unitOfWork;
        _ratingConverter = ratingConverter;
    }
    public async Task<IActionResult> CreateRatingAsync(CreateRatingDto dto, Guid userId)
    {
        var restaurant = await _unitOfWork.RestaurantRepository.GetByIdAsync(dto.RestaurantId);

        if (dto.RatingStars < 1 || dto.RatingStars > 5)
        {
            return new BadRequestObjectResult("Rating must be between 1 and 5.");
        }
        
        var existingRating = await _unitOfWork.RatingRepository.GetRatingByUserIdAsync(userId, dto.RestaurantId);

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
            AppUserId = userId,
            Restaurant = restaurant
        };

        await _unitOfWork.RatingRepository.InsertAsync(rating);
        await _unitOfWork.SaveChangesAsync();
        
        var ratings = await _unitOfWork.RatingRepository.GetRatingsAsync(dto.RestaurantId);
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
        var deletedRating = _ratingConverter.RatingToRatingDto(ratingToDelete);
        
        await _unitOfWork.RatingRepository.Delete(ratingId);
        await _unitOfWork.SaveChangesAsync();
        
        return new OkObjectResult(deletedRating);
    }

    public async Task<IActionResult> GetAllRatingsAsync(Guid restaurantId)
    {
        var ratings = await _unitOfWork.RatingRepository.GetRatingsAsync(restaurantId);
        var ratingDtos = _ratingConverter.RatingsToRatingDtos(ratings);
        
        return new OkObjectResult(ratingDtos);
    }

    public async Task<IActionResult> GetRatingByIdAsync(Guid ratingId)
    {
        var rating = await _unitOfWork.RatingRepository.GetByIdAsync(ratingId);
        return new OkObjectResult(rating);
    }
}