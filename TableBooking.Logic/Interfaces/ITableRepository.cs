namespace TableBooking.Logic.Interfaces;

using Model.Models;

public interface ITableRepository : IGenericRepository<Table>
{
    Task<IEnumerable<Table>> GetTablesByRestaurantIdAsync(Guid restaurantId);
    Task<Table?> GetAvailableTableAsync(Guid restaurantId, int amountOfPeople, DateTime bookingDate);
    Task<Table> GetTableByTableIdAsync(Guid tableId);
    Task<Guid> GetRestaurantIdByTableIdAsync(Guid tableId);
}