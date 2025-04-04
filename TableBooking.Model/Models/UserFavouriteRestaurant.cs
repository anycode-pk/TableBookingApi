namespace TableBooking.Model.Models;

public class UserFavouriteRestaurant
{
    public Guid UserId { get; set; }
    public AppUser User { get; set; }
    
    public Guid RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
}