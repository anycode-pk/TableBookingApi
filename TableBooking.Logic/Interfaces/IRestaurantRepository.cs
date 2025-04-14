namespace TableBooking.Logic.Interfaces;

using Model.Dtos.RestaurantDtos;
using Model.Models;

public interface IRestaurantRepository : IGenericRepository<Restaurant>
{
    public Task<RestaurantSearchResponseDto> SearchRestaurantsAsync(string? restaurantName, Price? price, 
        bool? searchForEmptyTablesOnly, DateTime? requestedDateTimeForEmptyTables,
        int? numberOfPeopleForEmptyTables, int page, int pageSize);
    public Task<Restaurant?> GetRestaurantByTableIdAsync(Guid tableId);
    public Task<Restaurant?> GetRestaurantByRestaurantIdAsync(Guid restaurantId);
    public Task<IEnumerable<Guid?>> GetAllRestaurantIds();
}