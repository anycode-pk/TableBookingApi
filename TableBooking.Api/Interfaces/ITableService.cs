namespace TableBooking.Api.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Model.Dtos.TableDtos;
using Model.Models;

public interface ITableService
{
    public Task<IActionResult> GetAllTablesAsync();
    public Task<IActionResult> GetTableByIdAsync(Guid tableId);
    public Task<IActionResult> GetTablesForRestaurantAsync(Guid restaurantId);
    public Task<IActionResult> CreateTableAsync(TableDto dto);
    public Task<IActionResult> UpdateTableAsync(TableDto dto, Guid tableId);
    public Task<IActionResult> DeleteTableAsync(Guid tableId);
    public Task<Table> GetTableObjectByIdAsync(Guid tableId);
}