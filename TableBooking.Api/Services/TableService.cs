using Microsoft.AspNetCore.Mvc;
using TableBooking.Logic.Interfaces;
using TableBooking.Model.Models;
using TableBooking.Api.Interfaces;
using TableBooking.Model.Dtos.TableDtos;
using TableBooking.Logic.Converters.TableConverters;

namespace TableBooking.Api.Services;

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
            
        if (tableToDelete == null)
            return new NotFoundObjectResult($"Restaurant with Id = {tableId} not found");
            
        // TODO: Remove bookings? but only future bookings?
            
        await _unitOfWork.TableRepository.Delete(tableToDelete.Id);
        await _unitOfWork.SaveChangesAsync();
            
        return new OkObjectResult(tableToDelete);
    }

    public async Task<IActionResult> GetAllTablesAsync()
    {
        var tables = await _unitOfWork.TableRepository.GetAllAsync();
        if (tables == null) return new BadRequestObjectResult("No tables found");
            
        foreach (var table in tables)
        {
            var bookings = await _unitOfWork.BookingRepository.GetBookingsByTableId(table.Id);
            table.Bookings = bookings ?? new List<Booking>();
        }
            
        return new OkObjectResult(_tableConverter.TablesToTableDtos(tables));
    }

    public async Task<IActionResult> GetTableByIdAsync(Guid tableId)
    {
        var table = await _unitOfWork.TableRepository.GetByIdAsync(tableId);
        if (table == null)
            return new BadRequestObjectResult($"Can't find table with {tableId}");
            
        var bookings = await _unitOfWork.BookingRepository.GetBookingsByTableId(table.Id);
        table.Bookings = bookings ?? new List<Booking>();
        return new OkObjectResult(_tableConverter.TableToTableDto(table));
    }
        
    public async Task<Table> GetTableObjectByIdAsync(Guid tableId)
    {
        var table = await _unitOfWork.TableRepository.GetByIdAsync(tableId);
        if (table == null)
            throw new BadHttpRequestException($"Table id: {tableId} doesn't exist.");
            
        var bookings = await _unitOfWork.BookingRepository.GetBookingsByTableId(table.Id);
        table.Bookings = bookings ?? new List<Booking>();
            
        return table;
    }

    public async Task<IActionResult> GetTablesForRestaurantAsync(Guid restaurantId)
    {
        var tables = await _unitOfWork.TableRepository.GetTablesByRestaurantIdAsync(restaurantId);
        if (tables == null) return new BadRequestObjectResult("No tables found");
            
        foreach (var table in tables)
        {
            var bookings = await _unitOfWork.BookingRepository.GetBookingsByTableId(table.Id);
            table.Bookings = bookings ?? new List<Booking>();
        }
            
        return new OkObjectResult(_tableConverter.TablesToTableDtos(tables));
    }

    public async Task<IActionResult> UpdateTableAsync(TableDto dto, Guid tableId)
    {
        var updateTable = await _unitOfWork.TableRepository.GetByIdAsync(tableId);
        if (updateTable == null)
            return new BadRequestObjectResult($"Booking with id {tableId} doesn't exist.");

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