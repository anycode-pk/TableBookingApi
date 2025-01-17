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
                .Where(x => x.RestaurantId.Equals(restaurantId))
                .ToListAsync();
        }

        public async Task<Table> GetTableByTableIdAsync(Guid tableId)
        {
            return (await _objectSet.FirstOrDefaultAsync(t => t.Id == tableId))!;
        }
    }
}
