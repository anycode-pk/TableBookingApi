namespace TableBooking.Logic;

using Interfaces;
using Model;
using Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly TableBookingContext _context;

    public UnitOfWork(TableBookingContext context)
    {
        _context = context;
    }

    public IBookingRepository BookingRepository => new BookingRepository(_context);

    public IRestaurantRepository RestaurantRepository => new RestaurantRepository(_context);

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