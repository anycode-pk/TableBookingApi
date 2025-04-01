namespace TableBooking.Model.Dtos.BookingDtos;

public class BookingDto
{
    public Guid Id { get; set; }
    private DateTime _date;

    public DateTime Date
    {
        get => _date;
        set => _date = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
    public int DurationInMinutes { get; set; }
    public int AmountOfPeople { get; set; }
    public Guid AppUserId { get; set; }
    public Guid RestaurantId { get; set; }
}