using Microsoft.EntityFrameworkCore;
using TableBooking.Logic.Extensions;
using TableBooking.Logic.Interfaces;
using TableBooking.Model;
using TableBooking.Model.Models;

namespace TableBooking.Logic.Repositories
{
    public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(TableBookingContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync(string? restaurantName, Price? price)
        {
            return await _objectSet
                .FilterByName(restaurantName)
                .FilterByPrice(price)
                .ToListAsync();
        }
    }
}
