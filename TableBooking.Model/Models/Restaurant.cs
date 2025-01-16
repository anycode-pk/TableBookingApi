using Microsoft.EntityFrameworkCore;

namespace TableBooking.Model.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum Price
    {
        LOW,
        MEDIUM,
        HIGH
    }
    public class Restaurant : Entity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string? Description { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }

        [Required]
        public string PrimaryImageURL { get; set; } =
            "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/240px-No_image_available.svg.png";
        [Required]
        public string SecondaryImageURL { get; set; } = 
            "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/240px-No_image_available.svg.png";
        [Precision(1,1)]
        public double Rating { get; set; }
        public Price Price { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public IEnumerable<Table> Tables { get; set; }
    }
}