using Microsoft.EntityFrameworkCore;
using TableBooking.Logic.Interfaces;
using TableBooking.Model;
using TableBooking.Model.Models;

namespace TableBooking.Logic.Repositories
{
    public class UserRepository : GenericRepository<AppUser>, IUserRepository
    {
        public UserRepository(TableBookingContext context) : base(context)
        {
        }
        
        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _objectSet.Include(x => x.Bookings).ToListAsync();
        }

        public async Task<AppUser> GetUserById(Guid userId)
        {
            return await _objectSet.Include(x => x.Bookings).FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
