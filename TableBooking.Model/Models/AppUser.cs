using Microsoft.AspNetCore.Identity;

namespace TableBooking.Model.Models;

using Dtos.UserDtos;

public class AppUser : IdentityUser<Guid>
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public IEnumerable<Booking> Bookings { get; set; }
    public Guid AppRoleId { get; set; }
    public AppRole AppRole { get; set; }
        
    public AppUserDto ToDto()
    {
        return new AppUserDto
        {
            Bookings = Bookings
        };
    }
}