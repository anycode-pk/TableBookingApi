namespace TableBooking.Model.Dtos.RestaurantDtos;

using Models;

public class RestaurantSearchResponseDto
{
    public IEnumerable<Restaurant> Restaurants { get; set; } = [];
    public List<string> Suggestions { get; set; } = [];
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}