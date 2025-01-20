namespace TableBooking.Model.Dtos.RatingDtos;

public class CreateRatingDto
{
    public int RatingStars { get; set; }
    public string? Comment { get; set; } = string.Empty;
    public Guid RestaurantId { get; set; }
}