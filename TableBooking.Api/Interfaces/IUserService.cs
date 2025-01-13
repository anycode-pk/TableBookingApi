using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TableBooking.Model.Dtos.UserDtos;

namespace TableBooking.Api.Interfaces
{
    using Model.Models;

    public interface IUserService
    {
        public Task<IActionResult> Register(UserRegisterDto userRegisterDTO);
        public Task<IActionResult> Login(UserLoginDto userLoginDTO, CancellationToken cancellationToken);
        public Task<AppUser> GetUserInfo(Guid id);
        public Task SeedRoles();
    }
}
