namespace TableBooking.Logic.Interfaces;

using Model.Models;

public interface ITableRepository : IGenericRepository<Table>
{
    Task<IEnumerable<Table>> GetTablesByRestaurantIdAsync(Guid restaurantId);
    Task<Table> GetTableByTableIdAsync(Guid tableId);
}