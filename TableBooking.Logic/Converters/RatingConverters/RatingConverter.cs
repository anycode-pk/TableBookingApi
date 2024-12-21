using TableBooking.Logic.Converters.UserConverters;
using TableBooking.Model.Dtos.RatingDtos;
using TableBooking.Model.Models;

namespace TableBooking.Logic.Converters.RatingConverters
{
    public class RatingConverter : IRatingConverter
    {
        private IShortUserInfoConverter _shortUserInfoConverter;

        public RatingConverter(IShortUserInfoConverter shortUserInfoConverter)
        {
            _shortUserInfoConverter = shortUserInfoConverter;
        }
        public IEnumerable<RatingDto> RatingsToRatingDtos(IEnumerable<Rating> ratings)
        {
            var ratingsDto = new List<RatingDto>();
            foreach (var rating in ratings) 
            {
                ratingsDto.Add(RatingToRatingDto(rating));
            }
            return ratingsDto;
        }
        public RatingDto RatingToRatingDto(Rating rating)
        {
            return new RatingDto
            {
                Id = rating.Id,
                Comment = rating.Comment,
                DateOfRating = rating.DateOfRating,
                NumberOfLikes = rating.NumberOfLikes,
                RatingStars = rating.RatingStars,
                User = _shortUserInfoConverter.UserToUserShortInfo(rating.AppUser),
                RestaurantId = rating.Restaurant.Id,
            };
        }
    }
}
