using Microsoft.EntityFrameworkCore;
using TableBooking.Logic.Interfaces;
using TableBooking.Model;
using TableBooking.Model.Models;

namespace TableBooking.Logic.Repositories
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        public RatingRepository(TableBookingContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Rating>> GetRatingsAsync(Guid restaurantId)
        {
            return await _objectSet
                .Include(x => x.Restaurant)
                .Include(x => x.AppUser)
                .Where(x => x.RestaurantId.Equals(restaurantId)).ToListAsync();
        }

        public async Task<Rating> GetRating(Guid id)
        {
            return await _objectSet
                .Include(x => x.Restaurant)
                .Include(x => x.AppUser)
                .FirstAsync(x => x.Id == id);
        }
    }
}
