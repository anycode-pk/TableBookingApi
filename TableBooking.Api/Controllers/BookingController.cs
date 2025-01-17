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
    [Authorize]
    public async Task<IActionResult> GetUserBookings()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("User ID not found in claims."));
            
        return await _bookingService.GetAllBookings(userId);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetUserBookingById(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("User ID not found in claims."));
            
        return await _bookingService.GetBookingByIdAsync(id, userId);
    }

    [HttpDelete("Delete/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteUserBooking(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("User ID not found in claims."));
        return await _bookingService.DeleteBookingAsync(id, userId);
    }

    [HttpPost("CreateBooking/{tableId}")]
    [Authorize]
    public async Task<IActionResult> CreateUserBooking([FromBody] CreateBookingDto bookingToCreateDto, Guid tableId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("User ID not found in claims."));
            
        return await _bookingService.CreateBookingAsync(bookingToCreateDto, userId, tableId);
    }

    [HttpPut("UpdateBooking/{bookingId}")]
    [Authorize]
    public async Task<IActionResult> UpdateUserBooking([FromBody] UpdateBookingDto updateBookingDto, Guid bookingId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("User ID not found in claims."));
        return await _bookingService.UpdateBookingAsync(updateBookingDto, userId, bookingId);
    }

}