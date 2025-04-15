namespace TableBooking.Model.Dtos.UserDtos;

using Models;

public class AppUserDto
{
    public IEnumerable<Booking> Bookings { get; set; } = new List<Booking>();
    public string RoleName { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? Username { get; set; } = string.Empty;
    public List<UserFavouriteRestaurant> FavouriteRestaurants { get; set; } = new();
}