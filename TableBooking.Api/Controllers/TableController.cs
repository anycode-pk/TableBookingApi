namespace TableBooking.Api.Controllers;

using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.TableDtos;

[Route("[controller]")]
[ApiController]
public class TableController : ControllerBase
{
    private readonly ITableService _tableService;
    public TableController(ITableService tableService)
    {
        _tableService = tableService;
    }

    [HttpGet("GetAllTables")]
    public async Task<IActionResult> GetAllTables()
    {
        return await _tableService.GetAllTablesAsync();
    }

    [HttpGet("GetTable/{id}")]
    public async Task<IActionResult> GetTableById(Guid id)
    {
        return await _tableService.GetTableByIdAsync(id);
    }

    [HttpGet("GetTableByRestaurant")]
    public async Task<IActionResult> GetTableByRestaurantId([FromQuery] Guid restaurantId)
    {
        return await _tableService.GetTablesForRestaurantAsync(restaurantId);
    }

    [HttpPost("CreateTable")]
    public async Task<IActionResult> CreateTable([FromBody] TableDto tableDto)
    {
        return await _tableService.CreateTableAsync(tableDto);
    }

    [HttpPut("UpdateTable/{tableId}")]
    public async Task<IActionResult> UpdateTable([FromBody] TableDto tableDto, Guid tableId)
    {
        return await _tableService.UpdateTableAsync(tableDto, tableId);
    }

    [HttpDelete("DeleteTable/{id:Guid}")]
    public async Task<IActionResult> DeleteTable(Guid id)
    {
        return await _tableService.DeleteTableAsync(id);
    }
}