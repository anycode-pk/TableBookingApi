using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TableBooking.Api.Interfaces;
using TableBooking.Model.Dtos.UserDtos;

namespace TableBooking.Controllers
{
    using Model.Models;

    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDTO)
        {
            return await _userService.Register(userRegisterDTO);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDTO)
        {
            return await _userService.Login(userLoginDTO);
        }
        
        [HttpPost]
        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            // TODO: implement logout
            throw new NotImplementedException();
        }
        
        [HttpGet]
        [Authorize]
        public async Task<AppUserDto> GetUserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            return await _userService.GetUserInfo(Guid.Parse(userId), CancellationToken.None);
        }
    }
}
