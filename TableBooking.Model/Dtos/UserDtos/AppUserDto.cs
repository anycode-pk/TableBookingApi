namespace TableBooking.Model.Dtos.UserDtos;

using Models;

public class AppUserDto
{
    public IEnumerable<Booking> Bookings { get; set; }
}