namespace TableBooking.Api.Controllers;

using System.Security.Claims;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.BookingDtos;

[Route("[controller]")]
[ApiController]
public class AdminRestaurantController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public AdminRestaurantController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }
    
    [HttpGet("Bookings")]
    [ProducesResponseType(typeof(List<BookingDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Restaurant")]
    public async Task<IActionResult> GetRestaurantBookings([FromQuery] Guid restaurantId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
    {
        return await _bookingService.GetBookingsForRestaurant(restaurantId, fromDate, toDate);
    }
}