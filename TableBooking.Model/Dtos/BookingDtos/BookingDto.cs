namespace TableBooking.Model.Dtos.BookingDtos;

public class BookingDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int DurationInMinutes { get; set; }
    public int AmountOfPeople { get; set; }
    public Guid AppUserId { get; set; }
    public Guid RestaurantId { get; set; }
}