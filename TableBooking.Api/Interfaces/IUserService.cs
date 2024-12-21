using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TableBooking.Model.Dtos.UserDtos;

namespace TableBooking.Api.Interfaces
{
    public interface IUserService
    {
        public Task<IActionResult> Register(UserRegisterDto userRegisterDTO);
        public Task<IActionResult> Login(UserLoginDto userLoginDTO);
        public Task SeedRoles();
    }
}
