namespace TableBooking.Model.Dtos.RestaurantDtos;

using System.ComponentModel.DataAnnotations;
using Models;

public class RestaurantShortInfoDto
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Description { get; set; }
    public string? Phone { get; set; }
    public string? Location { get; set; }

    [Required]
    public string SecondaryImageURL { get; set; } =
        "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/240px-No_image_available.svg.png";

    [Required]
    public string PrimaryImageURL { get; set; } =
        "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/240px-No_image_available.svg.png";

    public Price Price { get; set; } = Price.Medium;
    public DateTime OpenTime { get; set; }
    public DateTime CloseTime { get; set; }
}