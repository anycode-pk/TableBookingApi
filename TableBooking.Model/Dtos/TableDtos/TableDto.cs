using TableBooking.Model.Models;

namespace TableBooking.Model.Dtos.TableDtos
{
    public class TableDto
    {
        public int NumberOfSeats { get; set; }
        public Guid RestaurantId { get; set; }

    }
}