namespace TableBooking.Logic.Repositories;

using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Models;

public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{
    public BookingRepository(TableBookingContext context) : base(context) { }

    public async Task<IEnumerable<Booking>> GetAllBookingsForSpecificUserAsync(Guid userId, DateTime? from = null, DateTime? to = null)
    {
        return await ObjectSet.Where(x => x.AppUserId.Equals(userId))
            .Where(x => !from.HasValue || x.Date >= from.Value)
            .Where(x => !to.HasValue || x.Date <= to.Value)
            .ToListAsync();
    }

    public async Task<Booking?> GetBookingByIdForSpecificUserAsync(Guid bookingId, Guid userId)
    {
        return await ObjectSet.FirstOrDefaultAsync(x => x.Id.Equals(bookingId) && x.AppUserId.Equals(userId));
    }

    public async Task<IEnumerable<Booking>> GetBookingsByTableId(Guid tableId)
    {
        return await ObjectSet.Where(b => b.TableId == tableId).ToListAsync();
    }
    
    public async Task<IEnumerable<Booking>> GetBookingsForSpecificRestaurantAsync(Guid restaurantId, DateTime? from = null, DateTime? to = null)
    {
        return await ObjectSet
            .Where(b => b.RestaurantId == restaurantId)
            .Where(b => !from.HasValue || b.Date >= from.Value)
            .Where(b => !to.HasValue || b.Date <= to.Value)
            .ToListAsync();
    }
}