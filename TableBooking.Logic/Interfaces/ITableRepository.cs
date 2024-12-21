using TableBooking.Model.Models;

namespace TableBooking.Logic.Interfaces
{
    public interface ITableRepository : IGenericRepository<Table>
    {
        Task<IEnumerable<Table>> GetTablesByRestaurantIdAsync(Guid restaurantId);
    }
}
