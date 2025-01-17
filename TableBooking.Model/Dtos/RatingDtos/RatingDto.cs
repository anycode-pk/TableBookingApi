namespace TableBooking.Model.Dtos.RatingDtos;

using UserDtos;

public class RatingDto
{
    public Guid Id { get; set; }
    public int RatingStars { get; set; }
    public int NumberOfLikes { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime DateOfRating { get; set; }
    public Guid RestaurantId { get; set; }
    public UserShortInfoDto User { get; set; } = new();
}