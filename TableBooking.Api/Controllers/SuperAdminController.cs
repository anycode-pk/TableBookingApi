namespace TableBooking.Api.Controllers;

using Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class SuperAdminController : ControllerBase
{
    private readonly IBookingService _bookingService;
    private readonly IRestaurantService _restaurantService;

    public SuperAdminController(IBookingService bookingService, IRestaurantService restaurantService)
    {
        _bookingService = bookingService;
        _restaurantService = restaurantService;
    }
    
    // TODO:
    // Implement endpoints for super admin.
}


