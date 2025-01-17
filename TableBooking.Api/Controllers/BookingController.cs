using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TableBooking.Api.Interfaces;
using TableBooking.Api.Services;
using TableBooking.Model.Dtos.BookingDtos;
using TableBooking.Model.Models;


namespace TableBooking.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly UserManager<AppUser> _userManager;

        public BookingController(IBookingService bookingService, UserManager<AppUser> userManager)
        {
            _bookingService = bookingService;
            _userManager = userManager;
        }

        // private async Task<AppUser> GetCurrentUserAsync()
        // {
        //     //_userService.GetUserAsync(HttpContext.User);
        //     //_userService.
        // }


        [HttpGet("GetAllUserBookings")]
        [ProducesResponseType(typeof(List<BookingDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserBookings()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            return await _bookingService.GetAllBookings(userId);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetUserBookingById(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            return await _bookingService.GetBookingByIdAsync(id, userId);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUserBooking(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _bookingService.DeleteBookingAsync(id, userId);
        }

        [HttpPost("CreateBooking/{tableId}")]
        public async Task<IActionResult> CreateUserBooking([FromBody] CreateBookingDto bookingToCreateDto, Guid tableId)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            return await _bookingService.CreateBookingAsync(bookingToCreateDto, userId, tableId);
        }

        [HttpPut("UpdateBooking/{bookingId}")]
        public async Task<IActionResult> UpdateUserBooking([FromBody] UpdateBookingDto updateBookingDto, Guid bookingId)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _bookingService.UpdateBookingAsync(updateBookingDto, userId, bookingId);
        }

    }
}
