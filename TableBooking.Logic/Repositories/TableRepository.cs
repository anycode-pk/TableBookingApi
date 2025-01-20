namespace TableBooking.Logic.Repositories;

using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Models;

public class TableRepository : GenericRepository<Table>, ITableRepository
{
    public TableRepository(TableBookingContext context) : base(context)
    {
        }
    public async Task<IEnumerable<Table>> GetTablesByRestaurantIdAsync(Guid restaurantId)
    {
            return await ObjectSet
                .Where(x => x.RestaurantId.Equals(restaurantId))
                .Include(t => t.Bookings)
                .ToListAsync();
        }

    public async Task<Table?> GetAvailableTableAsync(Guid restaurantId, int amountOfPeople, DateTime bookingDate)
    {
        return await ObjectSet.Where(t =>
            t.Bookings != null && t.RestaurantId == restaurantId && t.NumberOfSeats >= amountOfPeople && t.Bookings.All(b => b.Date != bookingDate))
            .Include(t => t.Bookings)
            .FirstOrDefaultAsync();
    }

    public async Task<Table> GetTableByTableIdAsync(Guid tableId)
    {
        return (await ObjectSet.Include(t => t.Bookings).FirstOrDefaultAsync(t => t.Id == tableId))!;
    }

    public async Task<Guid> GetRestaurantIdByTableIdAsync(Guid tableId)
    {
        var table = await ObjectSet.FirstOrDefaultAsync(t => t.Id == tableId);

        return table?.RestaurantId ?? Guid.Empty;
    }
}