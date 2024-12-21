using Microsoft.AspNetCore.Mvc;
using TableBooking.Api.Interfaces;
using TableBooking.Api.Services;
using TableBooking.Model.Dtos.RatingDtos;

namespace TableBooking.Api.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet("GetAllRatings")]
        public async Task<IActionResult> GetRatings([FromQuery] Guid restuarantId)
        {
            return await _ratingService.GetAllRatingsAsync(restuarantId);
        }

        [HttpGet("GetRating/{id}")]
        public async Task<IActionResult> GetRatingById(Guid id)
        {
            return await _ratingService.GetRatingByIdAsync(id);
        }

        [HttpPost("CreateRating")]
        public async Task<IActionResult> CreateRating([FromBody] CreateRatingDto createRatingDto)
        {
            return await _ratingService.CreateRatingAsync(createRatingDto);
        }

        [HttpDelete("DeleteRating/{id}")]
        public async Task<IActionResult> DeleteRating(Guid id)
        {
            return await _ratingService.DeleteRatingAsync(id);
        }
    }
}
