using TableBooking.Model.Models;

namespace TableBooking.Logic.Interfaces
{
    public interface IRatingRepository : IGenericRepository<Rating>
    {
        Task<IEnumerable<Rating>> GetRatingsAsync(Guid restaurantId);
        Task<Rating?> GetRatingByUserIdAsync(Guid userId, Guid restaurantId);
        Task<Rating> GetRating(Guid id);
    }
}
