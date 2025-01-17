namespace TableBooking.Logic.Extensions;

using Model.Models;

public static class RestaurantExtension
{
    public static IQueryable<Restaurant> FilterByName(this IQueryable<Restaurant> restaurant, string? name)
    {
            if(string.IsNullOrEmpty(name))
            {
                return restaurant;
            }
            return restaurant.Where(x => x.Name.ToLower().StartsWith(name.ToLower()));
        }

    public static IQueryable<Restaurant> FilterByPrice(this IQueryable<Restaurant> restaurant, Price? price)
    {
            if (price == null)
            {
                return restaurant;
            }
            return restaurant.Where(x => x.Price.Equals(price));
        }
}