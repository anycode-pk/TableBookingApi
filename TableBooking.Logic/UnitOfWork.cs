using TableBooking.Logic.Interfaces;
using TableBooking.Logic.Repositories;
using TableBooking.Model;

namespace TableBooking.Logic
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private TableBookingContext _context;
        public UnitOfWork(TableBookingContext context) 
        {
            _context = context;
        }
        public IBookingRepository BookingRepository
        {
            get => new BookingRepository(_context);
        }

        public IRestaurantRepository RestaurantRepository
        {
            get => new RestaurantRepository(_context);
        }

        public ITableRepository TableRepository
        {
            get => new TableRepository(_context);
        }

        public IUserRepository UserRepository
        {
            get => new UserRepository(_context);
        }

        public IRatingRepository RatingRepository
        {
            get => new RatingRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}