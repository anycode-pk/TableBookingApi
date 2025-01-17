namespace TableBooking.Api.Controllers;

using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.TableDtos;

[Route("[controller]")]
[ApiController]
[Authorize]
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

    [HttpGet("GetTable/{tableId:guid}")]
    public async Task<IActionResult> GetTableById(Guid tableId)
    {
        return await _tableService.GetTableByIdAsync(tableId);
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

    [HttpPut("UpdateTable/{tableId:guid}")]
    public async Task<IActionResult> UpdateTable([FromBody] TableDto tableDto, Guid tableId)
    {
        return await _tableService.UpdateTableAsync(tableDto, tableId);
    }

    [HttpDelete("DeleteTable/{id:guid}")]
    public async Task<IActionResult> DeleteTable(Guid id)
    {
        return await _tableService.DeleteTableAsync(id);
    }
}