using TableBooking.Model.Models;

namespace TableBooking.Model.Dtos.TableDtos
{
    public class GetTablesDto
    {
        public Guid Id { get; set; }
        public int NumberOfSeats { get; set; }
        public Guid RestaurantId { get; set; }
        public IEnumerable<Booking>? Bookings { get; set; }
    }
}
