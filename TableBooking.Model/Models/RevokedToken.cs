namespace TableBooking.Model.Models;

public class RevokedToken : Entity
{
    public string Token { get; set; } = string.Empty;
    public DateTime RevokedAt { get; set; }
}