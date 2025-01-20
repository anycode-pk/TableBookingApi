namespace TableBooking.Logic.Interfaces;

using Model.Models;

public interface IRestaurantRepository : IGenericRepository<Restaurant>
{
    public Task<IEnumerable<Restaurant>> GetRestaurantsAsync(string? restaurantName, Price? price);
    public Task<Restaurant> GetRestaurantByTableIdAsync(Guid tableId);
    public Task<Restaurant> GetRestaurantByRestaurantIdAsync(Guid restaurantId);
    public Task<IEnumerable<Guid>> GetAllRestaurantIds();
}