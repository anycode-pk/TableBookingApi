namespace TableBooking.Logic.Converters.RatingConverters;

using Model.Dtos.RatingDtos;
using Model.Models;

public interface IRatingConverter
{
    IEnumerable<RatingDto> RatingsToRatingDtos(IEnumerable<Rating> ratings);
    public RatingDto RatingToRatingDto(Rating rating);
}