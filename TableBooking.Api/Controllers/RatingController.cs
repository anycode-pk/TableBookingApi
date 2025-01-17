namespace TableBooking.Api.Controllers;

using System.Security.Claims;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.RatingDtos;

[Route("[controller]")]
[ApiController]
[Authorize]
public class RatingController : ControllerBase
{
    private readonly IRatingService _ratingService;

    public RatingController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [HttpGet("GetAllRatings")]
    public async Task<IActionResult> GetRatings([FromQuery] Guid restaurantId)
    {
        return await _ratingService.GetAllRatingsAsync(restaurantId);
    }

    [HttpGet("GetRating/{id}")]
    public async Task<IActionResult> GetRatingById(Guid id)
    {
        return await _ratingService.GetRatingByIdAsync(id);
    }

    [HttpPost("CreateRating")]
    public async Task<IActionResult> CreateRating([FromBody] CreateRatingDto createRatingDto)
    {
        if (createRatingDto.AppUserId.HasValue)
            return await _ratingService.CreateRatingAsync(createRatingDto);

        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdString))
            return BadRequest("User ID could not be determined from the claims.");

        if (!Guid.TryParse(userIdString, out var userId))
            return BadRequest("Invalid User ID format in claims.");

        createRatingDto.AppUserId = userId;

        return await _ratingService.CreateRatingAsync(createRatingDto);
    }

    [HttpDelete("DeleteRating/{id}")]
    public async Task<IActionResult> DeleteRating(Guid id)
    {
        return await _ratingService.DeleteRatingAsync(id);
    }
}