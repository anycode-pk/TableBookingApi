using Microsoft.AspNetCore.Mvc;
using TableBooking.Model.Dtos.BookingDtos;

namespace TableBooking.Api.Interfaces
{
    public interface IBookingService
    {
        public Task<IActionResult> GetAllBookings(Guid userId);
        public Task<IActionResult> GetBookingByIdAsync(Guid bookingId, Guid userId);
        public Task<IActionResult> CreateBookingAsync(CreateBookingDto createBookingDto, Guid userId, Guid tableId);
        public Task<IActionResult> UpdateBookingAsync(UpdateBookingDto updateBookingDto, Guid userId, Guid bookingId);
        public Task<IActionResult> DeleteBookingAsync(Guid bookingId, Guid userId);
    }
}
