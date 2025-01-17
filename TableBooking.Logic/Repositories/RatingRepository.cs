namespace TableBooking.Logic.Repositories;

using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Models;

public class RatingRepository : GenericRepository<Rating>, IRatingRepository
{
    public RatingRepository(TableBookingContext context) : base(context)
    {
        }
    public async Task<IEnumerable<Rating>> GetRatingsAsync(Guid restaurantId)
    {
            return await ObjectSet
                .Include(x => x.Restaurant)
                .Include(x => x.AppUser)
                .Where(x => x.RestaurantId.Equals(restaurantId)).ToListAsync();
        }

    public async Task<Rating?> GetRatingByUserIdAsync(Guid userId, Guid restaurantId)
    {
            return await ObjectSet
                .Include(x => x.Restaurant)
                .Include(x => x.AppUser)
                .FirstOrDefaultAsync(x => x.AppUserId == userId && x.RestaurantId == restaurantId);
        }

    public async Task<Rating> GetRating(Guid id)
    {
            return await ObjectSet
                .Include(x => x.Restaurant)
                .Include(x => x.AppUser)
                .FirstAsync(x => x.Id == id);
        }
}