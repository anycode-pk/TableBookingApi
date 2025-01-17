namespace TableBooking.Logic.Repositories;

using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Models;

public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{
    public BookingRepository(TableBookingContext context) : base(context)
    {
        }

    public async Task<IEnumerable<Booking>> GetAllBookingsForSpecificUserAsync(Guid userId)
    {
            return await ObjectSet.Where(x => x.AppUserId.Equals(userId)).ToListAsync();
        }

    public async Task<Booking?> GetBookingByIdForSpecificUserAsync(Guid bookingId, Guid userId)
    {
            return await ObjectSet.FirstOrDefaultAsync(x => x.Id.Equals(bookingId) && x.AppUserId.Equals(userId));
        }

    public async Task<IEnumerable<Booking>> GetBookingsByTableId(Guid tableId)
    {
            return await ObjectSet.Where(b => b.TableId == tableId).ToListAsync();
        }
}