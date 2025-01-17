namespace TableBooking.Logic.Repositories;

using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Models;

public class TableRepository : GenericRepository<Table>, ITableRepository
{
    public TableRepository(TableBookingContext context) : base(context)
    {
        }
    public async Task<IEnumerable<Table>> GetTablesByRestaurantIdAsync(Guid restaurantId)
    {
            return await ObjectSet
                .Where(x => x.RestaurantId.Equals(restaurantId))
                .ToListAsync();
        }

    public async Task<Table> GetTableByTableIdAsync(Guid tableId)
    {
            return (await ObjectSet.FirstOrDefaultAsync(t => t.Id == tableId))!;
        }
}