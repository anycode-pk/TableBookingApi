namespace TableBooking.Model.Dtos.UserDtos;

using Models;

public class AppUserDto
{
    public IEnumerable<Booking> Bookings { get; set; } = new List<Booking>();
    public string? Email { get; set; } = string.Empty;
    public string? Username { get; set; } = string.Empty;
}