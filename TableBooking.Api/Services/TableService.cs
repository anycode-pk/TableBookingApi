namespace TableBooking.Api.Services;

using Interfaces;
using Logic.Converters.TableConverters;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.TableDtos;
using Model.Models;

public class TableService : ITableService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITableToGetConverter _tableConverter;

    public TableService(IUnitOfWork unitOfWork, ITableToGetConverter tableConverter)
    {
        _unitOfWork = unitOfWork;
        _tableConverter = tableConverter;
    }
    public async Task<IActionResult> CreateTableAsync(TableDto dto)
    {
        var table = new Table
        {
            NumberOfSeats = dto.NumberOfSeats,
            RestaurantId = dto.RestaurantId
        };
            
        await _unitOfWork.TableRepository.InsertAsync(table);
        await _unitOfWork.SaveChangesAsync();
            
        return new OkObjectResult(table);
    }

    public async Task<IActionResult> DeleteTableAsync(Guid tableId)
    {
        var tableToDelete = await _unitOfWork.TableRepository.GetByIdAsync(tableId);

        // TODO: Remove bookings? but only future bookings?
            
        await _unitOfWork.TableRepository.Delete(tableToDelete.Id);
        await _unitOfWork.SaveChangesAsync();
            
        return new OkObjectResult(tableToDelete);
    }

    public async Task<IActionResult> GetAllTablesAsync()
    {
        var tables = await _unitOfWork.TableRepository.GetAllAsync();

        var tablesList = tables.ToList();
        foreach (var table in tablesList)
        {
            var bookings = await _unitOfWork.BookingRepository.GetBookingsByTableId(table.Id);
            table.Bookings = bookings;
        }
            
        return new OkObjectResult(_tableConverter.TablesToTableDtos(tablesList));
    }

    public async Task<IActionResult> GetTableByIdAsync(Guid tableId)
    {
        var table = await _unitOfWork.TableRepository.GetByIdAsync(tableId);

        var bookings = await _unitOfWork.BookingRepository.GetBookingsByTableId(table.Id);
        table.Bookings = bookings;
        
        return new OkObjectResult(_tableConverter.TableToTableDto(table));
    }
        
    public async Task<Table> GetTableObjectByIdAsync(Guid tableId)
    {
        var table = await _unitOfWork.TableRepository.GetByIdAsync(tableId);
        if (table == null)
            throw new BadHttpRequestException($"Table id: {tableId} doesn't exist.");
            
        var bookings = await _unitOfWork.BookingRepository.GetBookingsByTableId(table.Id);
        table.Bookings = bookings;
            
        return table;
    }

    public async Task<IActionResult> GetTablesForRestaurantAsync(Guid restaurantId)
    {
        var tables = await _unitOfWork.TableRepository.GetTablesByRestaurantIdAsync(restaurantId);

        var tablesList = tables.ToList();
        foreach (var table in tablesList)
        {
            var bookings = await _unitOfWork.BookingRepository.GetBookingsByTableId(table.Id);
            table.Bookings = bookings;
        }
            
        return new OkObjectResult(_tableConverter.TablesToTableDtos(tablesList));
    }

    public async Task<IActionResult> UpdateTableAsync(TableDto dto, Guid tableId)
    {
        var updateTable = await _unitOfWork.TableRepository.GetByIdAsync(tableId);

        var table = new Table
        { 
            Id = updateTable.Id,
            NumberOfSeats = dto.NumberOfSeats,
            RestaurantId = dto.RestaurantId
        };
            
        await _unitOfWork.TableRepository.Update(table);
        await _unitOfWork.SaveChangesAsync();
            
        return new OkObjectResult(table);
    }
}