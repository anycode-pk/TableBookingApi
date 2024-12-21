using Microsoft.EntityFrameworkCore;
using TableBooking.Logic.Interfaces;
using TableBooking.Model;
using TableBooking.Model.Models;

namespace TableBooking.Logic.Repositories
{
    public class TableRepository : GenericRepository<Table>, ITableRepository
    {
        public TableRepository(TableBookingContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Table>> GetTablesByRestaurantIdAsync(Guid restaurantId)
        {
            return await _objectSet
                .Include(x => x.Restaurant)
                .Where(x => x.RestaurantId.Equals(restaurantId))
                .ToListAsync();
        }
    }
}
