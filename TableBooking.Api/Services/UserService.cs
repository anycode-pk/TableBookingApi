using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TableBooking.Logic.Interfaces;
using TableBooking.Model.Models;
using TableBooking.Api.Interfaces;
using TableBooking.Model.Dtos.UserDtos;
using TableBooking.Model.Dtos;

namespace TableBooking.Api.Services
{
    using Microsoft.EntityFrameworkCore;
    using Model;

    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly string userRoleId = "5ad1268f-f61f-4b1c-b690-cbf8c3d35019";
        private readonly TableBookingContext _dbContext;

        public UserService(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IConfiguration configuration,
            IUnitOfWork unitOfWork, 
            TableBookingContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            var userExists = await _userManager.FindByNameAsync(dto.Username);
            if (userExists != null)
                return new BadRequestObjectResult("Bad request: Registration failed");

            var appUserRole = await _roleManager.FindByIdAsync(userRoleId);
            if (appUserRole == null)
                return new BadRequestObjectResult("Bad request: Registration failed");

            var user = new AppUser()
            {
                Email = dto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = dto.Username,
                AppRoleId = appUserRole.Id
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return new BadRequestObjectResult("Invalid password lenght Or Bad Email");

            return new OkObjectResult(new ResultDto { Status = "Success", Message = "User created successfully!" });
        }

        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username) ;     
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return new UnauthorizedResult();
            }
            var role = await _roleManager.FindByIdAsync(user.AppRoleId.ToString());
            if (role == null) return new BadRequestObjectResult("Bad request");

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, role.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = GetToken(authClaims);

            return new OkObjectResult(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        public async Task<AppUserDto> GetUserInfo(Guid id, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            var userDto = user?.ToDto();

            return userDto ?? new();
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            
            return token;
        }

        public Task SeedRoles()
        {
            throw new NotImplementedException();
        }
    }
}
