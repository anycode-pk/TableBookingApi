namespace TableBooking.Logic.Repositories;

using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Models;

public class UserRepository : GenericRepository<AppUser>, IUserRepository
{
    public UserRepository(TableBookingContext context) : base(context)
    {
    }
        
    public async Task<IEnumerable<AppUser>> GetAllUsers()
    {
        return await ObjectSet.Include(x => x.Bookings).ToListAsync();
    }

    public async Task<AppUser> GetUserById(Guid userId)
    {
        return (await ObjectSet.Include(x => x.Bookings).FirstOrDefaultAsync(x => x.Id == userId))!;
    }
}