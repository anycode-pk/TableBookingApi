﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TableBooking.Api.Interfaces;
using TableBooking.Model.Dtos.BookingDtos;

namespace TableBooking.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("GetAllUserBookings")]
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

        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateUserBooking([FromBody] CreateBookingDto bookingToCreateDto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _bookingService.CreateBookingAsync(bookingToCreateDto, userId);
        }

        [HttpPut("UpdateBooking/{id}")]
        public async Task<IActionResult> UpdateUserBooking([FromBody] UpdateBookingDto updateBookignDto, Guid tableId)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _bookingService.UpdateBookingAsync(updateBookignDto, userId);
        }

    }
}
