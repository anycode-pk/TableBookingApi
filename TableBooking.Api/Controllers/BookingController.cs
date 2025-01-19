namespace TableBooking.Api.Controllers;

using System.Security.Claims;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.BookingDtos;

[Route("[controller]")]
[ApiController]
[Authorize]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet("GetAllUserBookings")]
    [ProducesResponseType(typeof(List<BookingDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserBookings()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("User ID not found in claims."));
            
        return await _bookingService.GetAllBookings(userId);
    }

    [HttpGet("GetById/{bookingId}")]
    public async Task<IActionResult> GetUserBookingById(Guid bookingId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("User ID not found in claims."));
            
        return await _bookingService.GetBookingByIdAsync(bookingId, userId);
    }

    [HttpDelete("Delete/{bookingId}")]
    public async Task<IActionResult> DeleteUserBooking(Guid bookingId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("User ID not found in claims."));
        return await _bookingService.DeleteBookingAsync(bookingId, userId);
    }

    [HttpPost("CreateBooking/{tableId}")]
    public async Task<IActionResult> CreateUserBooking([FromBody] CreateBookingDto bookingToCreateDto, Guid tableId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("User ID not found in claims."));
            
        return await _bookingService.CreateBookingAsync(bookingToCreateDto, userId, tableId);
    }
    
    [HttpPost("CreateBookingAutomatically/{restaurantId}")]
    public async Task<IActionResult> CreateUserBookingAutomaticByRestaurantId([FromBody] CreateBookingDto bookingToCreateDto, Guid restaurantId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("User ID not found in claims."));
            
        return await _bookingService.CreateAutomaticBookingByRestaurantIdAsync(bookingToCreateDto, userId, restaurantId);
    }

    [HttpPut("UpdateBooking/{bookingId}")]
    public async Task<IActionResult> UpdateUserBooking([FromBody] UpdateBookingDto updateBookingDto, Guid bookingId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("User ID not found in claims."));
        return await _bookingService.UpdateBookingAsync(updateBookingDto, userId, bookingId);
    }
}