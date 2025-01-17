using TableBooking.Model.Models;

namespace TableBooking.Logic.Interfaces
{
    public interface IRestaurantRepository : IGenericRepository<Restaurant>
    {
        public Task<IEnumerable<Restaurant>> GetRestaurantsAsync(string? restaurantName, Price? price);
        public Task<Restaurant> GetRestaurantByTableIdAsync(Guid tableId);
        public Task<IEnumerable<Guid>> GetAllRestaurantIds();
    }
}
