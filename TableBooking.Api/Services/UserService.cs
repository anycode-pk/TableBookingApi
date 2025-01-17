﻿namespace TableBooking.Api.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Extensions;
using Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.Dtos;
using Model.Dtos.UserDtos;
using Model.Models;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly TableBookingContext _dbContext;

    public UserService(UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        IConfiguration configuration, 
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
            return new BadRequestObjectResult($"User with the same username found: {dto.Username}.");
        
        var emailExists = await _userManager.FindByEmailAsync(dto.Email);
        if (emailExists != null)
            return new BadRequestObjectResult($"User with the same email found: {dto.Email}.");

        var appUserRole = await _roleManager.FindByNameAsync("User");
        if (appUserRole == null)
            return new BadRequestObjectResult($"Can't find role by name 'User'.");

        var user = new AppUser
        {
            Email = dto.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = dto.Username,
            AppRoleId = appUserRole.Id,
            AppRole =  appUserRole
        };
        
        var result = await _userManager.CreateAsync(user, dto.Password);
        
        if (!result.Succeeded)
            return new BadRequestObjectResult("Invalid password length or Bad Email");

        return new OkObjectResult(new ResultDto { Status = "Success", Message = "User created successfully!" });
    }

    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.Username);
        if (user == null)
            return new BadRequestObjectResult($"User with username '{dto.Username}' does not exist.");
        
        if (!await _userManager.CheckPasswordAsync(user, dto.Password))
            return new BadRequestObjectResult($"Wrong password.");
        
        var role = await _roleManager.FindByNameAsync("User");
        if (role == null) return new BadRequestObjectResult($"Can't login. Role named 'User' is not found.");

        if (string.IsNullOrEmpty(user.UserName))
        {
            return new BadRequestObjectResult($"User does not have a name. UserId {user.Id}");
        }

        if (string.IsNullOrEmpty(role.Name))
        {
            return new BadRequestObjectResult($"Role does not have a name. RoleId {role.Id}");
        }
        
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, role.Name),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = GetToken(authClaims);
        
        return new OkObjectResult(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo
        });
    }

    public async Task<IActionResult> Logout(string? authHeader)
    {
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            return new BadRequestObjectResult("Invalid authorization header.");
        }

        var token = authHeader.Substring("Bearer ".Length).Trim();

        var revokedToken = new RevokedToken
        {
            Token = token,
            RevokedAt = DateTime.UtcNow
        };

        _dbContext.RevokedTokens.Add(revokedToken);
        await _dbContext.SaveChangesAsync();

        return new OkObjectResult(revokedToken);
    }

    public async Task<AppUserDto> GetUserInfo(Guid id, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        var userDto = user?.ToDto();

        return userDto ?? new();
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? string.Empty));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
            
        return token;
    }
}