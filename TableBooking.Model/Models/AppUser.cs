namespace TableBooking.Model.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser<Guid>
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public IEnumerable<Booking> Bookings { get; set; } = new List<Booking>();
    public Guid AppRoleId { get; set; }
    public AppRole AppRole { get; set; } = new();
    public AppUserDto ToDto()
    {
        return new AppUserDto
        {
            Bookings = Bookings,
            Email = Email,
            Username = UserName
        };
    }
}