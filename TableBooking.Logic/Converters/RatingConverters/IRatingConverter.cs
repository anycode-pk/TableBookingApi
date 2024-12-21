using TableBooking.Model.Dtos.RatingDtos;
using TableBooking.Model.Models;

namespace TableBooking.Logic.Converters.RatingConverters
{
    public interface IRatingConverter
    {
        IEnumerable<RatingDto> RatingsToRatingDtos(IEnumerable<Rating> ratings);
        public RatingDto RatingToRatingDto(Rating rating);
    }
}
