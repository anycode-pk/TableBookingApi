namespace TableBooking.Api.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.UserDtos;

public interface IUserService
{
    public Task<IActionResult> Register(UserRegisterDto userRegisterDto);
    public Task<IActionResult> Login(UserLoginDto userLoginDto);
    public Task<IActionResult> Logout(string? authHeader);
    public Task<AppUserDto> GetUserInfo(Guid id, CancellationToken cancellationToken);
}