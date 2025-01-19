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
            AmountOfPeople = request.AmountOfPeople,
            Id = Guid.NewGuid(),
            RestaurantId = table.RestaurantId
        };

        await _unitOfWork.BookingRepository.InsertAsync(newBooking);
        await _unitOfWork.SaveChangesAsync();

        var bookingDto = new BookingDto
        {
            Id = newBooking.Id,
            Date = newBooking.Date,
            DurationInMinutes = newBooking.DurationInMinutes,
            AmountOfPeople = newBooking.AmountOfPeople,
            AppUserId = userId,
            RestaurantId = newBooking.RestaurantId
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
        
        if (booking.RestaurantId == Guid.Empty)
        {
            var restaurantId= await _unitOfWork.TableRepository.GetRestaurantIdByTableIdAsync(booking.TableId);

            booking.RestaurantId = restaurantId;
            await _unitOfWork.BookingRepository.Update(booking);
        }
        
        var bookingDto = new BookingDto
        {
            Id = booking.Id,
            Date = booking.Date,
            DurationInMinutes = booking.DurationInMinutes,
            AmountOfPeople = booking.AmountOfPeople,
            AppUserId = userId,
            RestaurantId = booking.RestaurantId
        };
        return new OkObjectResult(bookingDto);
    }

    public async Task<IActionResult> GetAllBookings(Guid userId)
    {
        var bookings = await _unitOfWork.BookingRepository.GetAllBookingsForSpecificUserAsync(userId);

        foreach (var booking in bookings)
        {
            if (booking.RestaurantId == Guid.Empty)
            {
                
                var restaurantId= await _unitOfWork.TableRepository.GetRestaurantIdByTableIdAsync(booking.TableId);

                booking.RestaurantId = restaurantId;
                await _unitOfWork.BookingRepository.Update(booking);
            }
        }
        
        return new OkObjectResult(bookings);
    }

    public async Task<IActionResult> UpdateBookingAsync(UpdateBookingDto updateBookingDto, Guid userId, Guid bookingId)
    {
        var booking = await _unitOfWork.BookingRepository.GetBookingByIdForSpecificUserAsync(bookingId, userId);
        if (booking == null)
            return new BadRequestObjectResult($"Booking with id {bookingId} doesn't exist.");
        
        // TODO: change tableId when user changed amount of people.
            
        var newBooking = new Booking
        {
            Id = booking.Id,
            Date = updateBookingDto.Date,
            DurationInMinutes = updateBookingDto.DurationInMinutes,
            AmountOfPeople = updateBookingDto.AmountOfPeople,
            TableId = booking.TableId,
            AppUserId = userId,
            RestaurantId = booking.RestaurantId
        };

        await _unitOfWork.BookingRepository.Update(newBooking);
        await _unitOfWork.SaveChangesAsync();

        return new OkObjectResult(newBooking);
    }
}