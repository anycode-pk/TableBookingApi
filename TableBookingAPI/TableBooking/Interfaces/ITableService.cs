using Microsoft.AspNetCore.Mvc;
using TableBooking.Model.Dtos.TableDtos;
using TableBooking.Model.Models;

namespace TableBooking.Api.Interfaces
{
    public interface ITableService
    {
        public Task<IActionResult> GetAllTablesAsync();
        public Task<IActionResult> GetTableByIdAsync(Guid tableId);
        public Task<IActionResult> GetTableByRestaurantAsync(Guid restaurantId);
        public Task<IActionResult> CreateTableAsync(TableDto dto);
        public Task<IActionResult> UpdateTableAsync(TableDto dto, Guid tableId);
        public Task<IActionResult> DeleteTableAsync(Guid tableId);
        public Task<Table> GetTableObjectByIdAsync(Guid tableId);
    }
}
