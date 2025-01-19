namespace TableBooking.Api.Extensions;

using Microsoft.AspNetCore.Identity;
using Model.Models;

public static class RolesExtension
{
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new AppRole { Name = "User" });
        }

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new AppRole { Name = "Admin" });
        }
        
        if (!await roleManager.RoleExistsAsync("Restaurant"))
        {
            await roleManager.CreateAsync(new AppRole { Name = "Restaurant" });
        }
    }
}