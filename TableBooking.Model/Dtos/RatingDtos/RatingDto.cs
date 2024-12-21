using TableBooking.Model.Dtos.UserDtos;

namespace TableBooking.Model.Dtos.RatingDtos
{
    public class RatingDto
    {
        public Guid Id { get; set; }
        public int RatingStars { get; set; }
        public int NumberOfLikes { get; set; }
        public string Comment { get; set; }
        public DateTime DateOfRating { get; set; }
        public Guid RestaurantId { get; set; }
        public UserShortInfoDto User { get; set; }
    }
}
