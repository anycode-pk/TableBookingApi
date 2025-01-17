namespace TableBooking.Model.Dtos.BookingDtos;

public class UpdateBookingDto
{
    public DateTime Date { get; set; }
    public int DurationInMinutes { get; set; }
    public int AmountOfPeople { get; set; }
}