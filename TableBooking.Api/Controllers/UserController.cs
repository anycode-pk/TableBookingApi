namespace TableBooking.Api.Controllers;

using System.Security.Claims;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.UserDtos;

[Route("[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        return await _userService.Register(userRegisterDto);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto) 
    {
        return await _userService.Login(userLoginDto);
    }
        
    [HttpPost]
    [Authorize]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

        return await _userService.Logout(authHeader);
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