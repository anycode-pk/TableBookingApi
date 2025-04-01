namespace TableBooking.Model.Models;

using Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser<Guid>
{
    public string? RefreshToken { get; set; }
    private DateTime? _refreshTokenExpiryTime { get; init; }
    public DateTime? RefreshTokenExpiryTime
    {
        get => _refreshTokenExpiryTime;
        init => _refreshTokenExpiryTime = value.HasValue ?
            DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) :
            null;
    }
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