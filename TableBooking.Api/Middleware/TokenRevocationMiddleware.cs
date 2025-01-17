namespace TableBooking.Api.Middleware;

using Microsoft.EntityFrameworkCore;
using Model;

public class TokenRevocationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly TableBookingContext _dbContext;

    public TokenRevocationMiddleware(RequestDelegate next, TableBookingContext dbContext)
    {
        _next = next;
        _dbContext = dbContext;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var authHeader = context.Request.Headers.Authorization.FirstOrDefault();
        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();
            var isRevoked = await _dbContext.RevokedTokens.AnyAsync(rt => rt.Token == token);

            if (isRevoked)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token has been revoked.");
                return;
            }
        }

        await _next(context);
    }
}