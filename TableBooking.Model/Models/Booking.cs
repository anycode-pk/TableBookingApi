namespace TableBooking.Model.Models;

public class Booking : Entity
{
    private DateTime _date { get; set; }

    public DateTime Date
    {
        get => _date;
        set => _date = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }

    public int DurationInMinutes { get; set; }
    public int AmountOfPeople { get; set; }
    public Guid AppUserId { get; set; }
    public Guid TableId { get; set; }
    public Guid RestaurantId { get; set; }
}