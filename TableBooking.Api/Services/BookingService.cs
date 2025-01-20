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
        
        table.Bookings?.ToList().Add(newBooking);
        var restaurant = await _unitOfWork.RestaurantRepository.GetRestaurantByRestaurantIdAsync(newBooking.RestaurantId);

        await _unitOfWork.BookingRepository.InsertAsync(newBooking);
        await _unitOfWork.TableRepository.Update(table);
        await _unitOfWork.RestaurantRepository.Update(restaurant);

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

    public async Task<IActionResult> CreateAutomaticBookingByRestaurantIdAsync(CreateBookingDto createBookingDto, Guid userId, Guid restaurantId)
    {
        // TODO: Assign 17:30 and not like 17:36:35Z ... 
        var availableTable = await _unitOfWork.TableRepository.GetAvailableTableAsync(restaurantId, createBookingDto.AmountOfPeople, createBookingDto.Date);

        if (availableTable == null)
        {
            return new BadRequestObjectResult($"No available table for {createBookingDto.AmountOfPeople} people at restaurant {restaurantId}");
        }

        var newBooking = new Booking
        {
            Date = createBookingDto.Date,
            DurationInMinutes = createBookingDto.DurationInMinutes,
            TableId = availableTable.Id,
            AppUserId = userId,
            AmountOfPeople = createBookingDto.AmountOfPeople,
            Id = Guid.NewGuid(),
            RestaurantId = availableTable.RestaurantId
        };
        
        availableTable.Bookings?.ToList().Add(newBooking);
        var restaurant = await _unitOfWork.RestaurantRepository.GetRestaurantByRestaurantIdAsync(restaurantId);
        restaurant.Tables.ToList().Add(availableTable);

        await _unitOfWork.BookingRepository.InsertAsync(newBooking);
        await _unitOfWork.TableRepository.Update(availableTable);
        await _unitOfWork.RestaurantRepository.Update(restaurant);
        
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

        var restaurant = await _unitOfWork.RestaurantRepository.GetRestaurantByRestaurantIdAsync(booking.RestaurantId);
        var table = await _unitOfWork.TableRepository.GetTableByTableIdAsync(newBooking.TableId);
        table.Bookings?.ToList().Add(newBooking);
        restaurant.Tables.ToList().Add(table);
        
        await _unitOfWork.BookingRepository.Update(newBooking);
        await _unitOfWork.TableRepository.Update(table);
        await _unitOfWork.RestaurantRepository.Update(restaurant);
        await _unitOfWork.SaveChangesAsync();

        return new OkObjectResult(newBooking);
    }
}