namespace TableBooking.Api.Services;

using Interfaces;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.BookingDtos;
using Model.Models;

public class BookingService : IBookingService
{
    private readonly IUnitOfWork _unitOfWork;
    public BookingService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IActionResult> CreateBookingAsync(CreateBookingDto request, Guid userId, Guid tableId)
    {
        var table = await _unitOfWork.TableRepository.GetTableByTableIdAsync(tableId);

        if (request.AmountOfPeople != table.NumberOfSeats)
        {
            return new BadRequestObjectResult($"Number of seats for this table is {table.NumberOfSeats}, but user has {request.AmountOfPeople} people.");
        }
            
        var newBooking = new Booking
        {
            Date = request.Date,
            DurationInMinutes = request.DurationInMinutes,
            TableId = table.Id,
            AppUserId = userId,
            AmountOfPeople = request.AmountOfPeople
        };

        await _unitOfWork.BookingRepository.InsertAsync(newBooking);
        await _unitOfWork.SaveChangesAsync();

        var bookingDto = new BookingDto
        {
            Id = newBooking.Id,
            Date = newBooking.Date,
            DurationInMinutes = newBooking.DurationInMinutes,
            AmountOfPeople = newBooking.AmountOfPeople,
            AppUserId = userId
        };
        return new CreatedResult(string.Empty, bookingDto);
    }

    public async Task<IActionResult> DeleteBookingAsync(Guid bookingId, Guid userId)
    {
        var booking = await _unitOfWork.BookingRepository.GetBookingByIdForSpecificUserAsync(bookingId, userId);
        if (booking == null)
            return new BadRequestObjectResult("Bad request");

        await _unitOfWork.BookingRepository.Delete(booking.Id);
        await _unitOfWork.SaveChangesAsync();
        
        return new NoContentResult();
    }

    public async Task<IActionResult> GetBookingByIdAsync(Guid bookingId, Guid userId)
    {
        
        var booking = await _unitOfWork.BookingRepository.GetBookingByIdForSpecificUserAsync(bookingId, userId);

        if (booking == null)
            return new BadRequestObjectResult("Bad request: no bookings");
        var bookingDto = new BookingDto
        {
            Id = booking.Id,
            Date = booking.Date,
            DurationInMinutes = booking.DurationInMinutes,
            AmountOfPeople = booking.AmountOfPeople,
            AppUserId = userId
        };
        return new OkObjectResult(bookingDto);
    }

    public async Task<IActionResult> GetAllBookings(Guid userId)
    {
        var bookings = await _unitOfWork.BookingRepository.GetAllBookingsForSpecificUserAsync(userId);
        
        return new OkObjectResult(bookings);
    }

    public async Task<IActionResult> UpdateBookingAsync(UpdateBookingDto updateBookingDto, Guid userId, Guid bookingId)
    {
        var booking = await _unitOfWork.BookingRepository.GetBookingByIdForSpecificUserAsync(bookingId, userId);
        if (booking == null)
            return new BadRequestObjectResult($"Booking with id {bookingId} doesn't exist.");
            
        var newBooking = new Booking
        {
            Id = booking.Id,
            Date = updateBookingDto.Date,
            DurationInMinutes = updateBookingDto.DurationInMinutes,
            AmountOfPeople = updateBookingDto.AmountOfPeople,
            TableId = booking.TableId,
            AppUserId = userId
        };

        await _unitOfWork.BookingRepository.Update(newBooking);
        await _unitOfWork.SaveChangesAsync();

        return new OkObjectResult(newBooking);
    }
}