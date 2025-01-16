using TableBooking.Model.Models;

namespace TableBooking.Model.Dtos.RestaurantDtos
{
    public class RestaurantShortInfoDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }

        public string SecondaryImageURL { get; set; } =
            "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/240px-No_image_available.svg.png";

        public string PrimaryImageURL { get; set; } =
            "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/240px-No_image_available.svg.png";
        public int Rating { get; set; }
        public Price Price { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
    }
}