namespace TableBooking.Api.Services;

using System.Security.Claims;
using Interfaces;
using Microsoft.AspNetCore.Authentication;

public class ClaimsTransformationService : IClaimsTransformation
{
    private readonly IUserService _userService;
    
    public ClaimsTransformationService(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity?.IsAuthenticated != true)
        {
            return principal;
        }
        
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        var userRoles = await _userService.GetUserRoles(userId);

        if (userRoles.Count == 0)
        {
            return principal;
        }

        foreach (var role in userRoles)
        {
            if (principal.HasClaim(ClaimTypes.Role, role))
            {
                continue;
            }
            
            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, role));
        }
        
        return principal;
    }
}