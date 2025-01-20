namespace TableBooking.Logic.Repositories;

using Extensions;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Models;

public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
{
    public RestaurantRepository(TableBookingContext context) : base(context) { }
    public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync(string? restaurantName, Price? price)
    {
        return await ObjectSet
            .FilterByName(restaurantName)
            .FilterByPrice(price)
            .Include(r => r.Tables)
            .ThenInclude(t => t.Bookings)
            .ToListAsync();
    }

    public async Task<Restaurant> GetRestaurantByTableIdAsync(Guid tableId)
    {
        return (await ObjectSet.Include(r => r.Tables).ThenInclude(t => t.Bookings)
            .FirstOrDefaultAsync(r => r.Tables.Any(t => t.Id == tableId)))!;
    }

    public async Task<Restaurant> GetRestaurantByRestaurantIdAsync(Guid restaurantId)
    {
        return (await ObjectSet.Include(r => r.Tables).ThenInclude(t => t.Bookings)
            .FirstOrDefaultAsync(r => r.Tables.Any(t => t.RestaurantId == restaurantId)))!;
    }

    public async Task<IEnumerable<Guid>> GetAllRestaurantIds()
    {
        return await ObjectSet
            .Select(r => r.Id)
            .ToListAsync();
    }
}