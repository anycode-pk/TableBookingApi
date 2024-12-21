namespace TableBooking.Model.Models
{
    public class Table : Entity
    {
        public int NumberOfSeats { get; set; }
        public Guid RestaurantId { get; set; }
        public IEnumerable<Booking>? Bookings { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
