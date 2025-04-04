namespace TableBooking.Logic.Interfaces;

using Model.Models;

public interface IBookingRepository : IGenericRepository<Booking>
{
    public Task<IEnumerable<Booking>> GetAllBookingsForSpecificUserAsync(Guid userId, DateTime? from = null, DateTime? to = null);
    public Task<Booking?> GetBookingByIdForSpecificUserAsync(Guid bookingId, Guid userId);
    public Task<IEnumerable<Booking>> GetBookingsByTableId(Guid tableId);
}