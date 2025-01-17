using Microsoft.EntityFrameworkCore;
using TableBooking.Logic.Interfaces;
using TableBooking.Model;
using TableBooking.Model.Models;

namespace TableBooking.Logic.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(TableBookingContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsForSpecificUserAsync(Guid userId)
        {
            return await _objectSet.Where(x => x.AppUserId.Equals(userId)).ToListAsync();
        }

        public async Task<Booking?> GetBookingByIdForSpecificUserAsync(Guid bookingId, Guid userId)
        {
            return await _objectSet.FirstOrDefaultAsync(x => x.Id.Equals(bookingId) && x.AppUserId.Equals(userId));
        }

        public async Task<IEnumerable<Booking>> GetBookingsByTableId(Guid tableId)
        {
            return await _objectSet.Where(b => b.TableId == tableId).ToListAsync();
        }
    }
}
