namespace TableBooking.Logic.Repositories;

using Extensions;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model;
using Model.Models;
using Settings;

public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
{
    public RestaurantRepository(TableBookingContext context, BookingSettings settings) : base(context)
    {
        _defaultDurationOfBooking = settings.DefaultDurationOfBooking;
    }
    private readonly int _defaultDurationOfBooking;
    public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync(string? restaurantName, Price? price,
        bool? searchForEmptyTablesOnly, DateTime? requestedDateTimeForEmptyTables,
        int? numberOfPeopleForEmptyTables)
    {
        var restaurants = await ObjectSet
            .FilterByName(restaurantName)
            .FilterByPrice(price)
            .Include(r => r.Tables)
            .ThenInclude(t => t.Bookings)
            .ToListAsync();
        
        if (searchForEmptyTablesOnly == true)
        {
            var requestedStart = requestedDateTimeForEmptyTables ?? DateTime.UtcNow;
            var requestedEnd = requestedStart.AddHours(2);

            restaurants = restaurants
                .Where(r => r.Tables.Any(t =>
                    (numberOfPeopleForEmptyTables == null || t.NumberOfSeats == numberOfPeopleForEmptyTables) &&
                    !t.Bookings.Any(b =>
                            b.Date < requestedEnd &&
                            b.Date.AddHours(_defaultDurationOfBooking) > requestedStart
                        )))
                .ToList();  
        }

        return restaurants;
    }
    
    public async Task<Restaurant?> GetRestaurantByTableIdAsync(Guid tableId)
    {
        return (await ObjectSet.Include(r => r.Tables).ThenInclude(t => t.Bookings)
            .FirstOrDefaultAsync(r => r.Tables.Any(t => t.Id == tableId)))!;
    }
    
    public async Task<Restaurant?> GetRestaurantByRestaurantIdAsync(Guid restaurantId)
    {
        return (await ObjectSet.Include(r => r.Tables).ThenInclude(t => t.Bookings)
            .FirstOrDefaultAsync(r => r.Tables.Any(t => t.RestaurantId == restaurantId)))!;
    }
    
    public async Task<IEnumerable<Guid?>> GetAllRestaurantIds()
    {
        return await ObjectSet
            .Select(r => (Guid?)r.Id)
            .ToListAsync();
    }
}