namespace TableBooking.Logic.Extensions;

using Model.Models;

public static class RestaurantExtension
{
    public static IQueryable<Restaurant> FilterByName(this IQueryable<Restaurant> restaurant, string? name)
    {
        return string.IsNullOrEmpty(name)
            ? restaurant
            : restaurant.Where(x => x.Name.ToLower().Contains(name.ToLower()));
    }

    public static IQueryable<Restaurant> FilterByPrice(this IQueryable<Restaurant> restaurant, Price? price)
    {
        return price == null ? restaurant : restaurant.Where(x => x.Price.Equals(price));
    }
    
    public static IQueryable<Restaurant> FilterByIsEmptyTables(this IQueryable<Restaurant> restaurants, bool? searchForEmptyTablesOnly)
    {
        return searchForEmptyTablesOnly == true
            ? restaurants.Where(r => r.Tables.Any(t => !(t.Bookings ?? Array.Empty<Booking>()).Any(b => b.Date >= DateTime.UtcNow)))
            : restaurants;
    }
}