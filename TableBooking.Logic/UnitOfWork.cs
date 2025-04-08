namespace TableBooking.Logic;

using Interfaces;
using Microsoft.Extensions.Options;
using Model;
using Repositories;
using Settings;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly TableBookingContext _context;
    private readonly BookingSettings _bookingSettings;

    public UnitOfWork(TableBookingContext context, IOptions<BookingSettings> bookingSettings)
    {
        _context = context;
        _bookingSettings = bookingSettings.Value;
    }

    public IBookingRepository BookingRepository => new BookingRepository(_context);

    public IRestaurantRepository RestaurantRepository => new RestaurantRepository(_context, _bookingSettings);

    public ITableRepository TableRepository => new TableRepository(_context);

    public IUserRepository UserRepository => new UserRepository(_context);

    public IRatingRepository RatingRepository => new RatingRepository(_context);

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}