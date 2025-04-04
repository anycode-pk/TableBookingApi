namespace TableBooking.Model.Dtos.BookingDtos;

public class CreateBookingDto
{
    private DateTime _date;

    public DateTime Date
    {
        get => _date;
        set => _date = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }

    public int DurationInMinutes { get; set; }
    public int AmountOfPeople { get; set; }
}