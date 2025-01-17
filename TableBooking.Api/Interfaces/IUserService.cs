namespace TableBooking.Api.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Model.Dtos.UserDtos;

public interface IUserService
{
    public Task<IActionResult> Register(UserRegisterDto userRegisterDTO);
    public Task<IActionResult> Login(UserLoginDto userLoginDTO);
    public Task<AppUserDto> GetUserInfo(Guid id, CancellationToken cancellationToken);
    public Task SeedRoles();
}