namespace TableBooking.Model.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public enum Price
{
    Low,
    Medium,
    High
}
public class Restaurant : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    [Required]
    public string PrimaryImageUrl { get; set; } =
        "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/240px-No_image_available.svg.png";
    [Required]
    public string SecondaryImageUrl { get; set; } = 
        "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/240px-No_image_available.svg.png";
    [Precision(1,1)]
    public double Rating { get; set; }
    public Price Price { get; set; }
    public DateTime OpenTime { get; set; }
    public DateTime CloseTime { get; set; }
    public IEnumerable<Table> Tables { get; set; } = new List<Table>();
}