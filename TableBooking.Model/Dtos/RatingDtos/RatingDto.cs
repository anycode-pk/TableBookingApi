namespace TableBooking.Model.Dtos.RatingDtos;

using UserDtos;

public class RatingDto
{
    public Guid Id { get; set; }
    public int RatingStars { get; set; }
    public int NumberOfLikes { get; set; }
    public string Comment { get; set; } = string.Empty;
    private DateTime _dateOfRating { get; set; }

    public DateTime DateOfRating
    {
        get => _dateOfRating;
        set => _dateOfRating = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }

    public Guid RestaurantId { get; set; }
    public UserShortInfoDto User { get; set; } = new();
}