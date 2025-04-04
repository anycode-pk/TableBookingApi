namespace TableBooking.Model.Models;

public class Rating : Entity
{
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
    public Guid AppUserId { get; set; }
    public Restaurant Restaurant { get; set; } = new();
    public AppUser AppUser { get; set; } = new();
}