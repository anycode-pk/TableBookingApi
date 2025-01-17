using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using TableBooking.Api.Configuration.DbSetup;
using TableBooking.Api.Configuration.HealthCheck;
using TableBooking.Api.Extensions;
using TableBooking.Api.Interfaces;
using TableBooking.Api.Middleware;
using TableBooking.Api.Services;
using TableBooking.Logic;
using TableBooking.Logic.Converters.RatingConverters;
using TableBooking.Logic.Converters.TableConverters;
using TableBooking.Logic.Converters.UserConverters;
using TableBooking.Logic.Interfaces;
using TableBooking.Model;
using TableBooking.Model.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TableBooking API",
        Version = "v0.0.1",
        Description = "Application created by AnyCode Students Club at Koszalin University of Technology",
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Type = SecuritySchemeType.Http,
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        }
                    },
                    new string[] { }
                }
                });
});

builder.Services.AddCors(p => p.AddPolicy("cors", corsPolicyBuilder =>
{
    corsPolicyBuilder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Host.UseSerilog((builderContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(builderContext.Configuration);
});

builder.Services.AddDbContext<TableBookingContext>(o =>
{
    var connectionString = builder.Configuration.GetConnectionString("TableBookingConnStr");
    
    var dbHost = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DB_HOST")) 
        ? Environment.GetEnvironmentVariable("DB_HOST") 
        : "localhost";

    var dbPort = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DB_PORT")) 
        ? Environment.GetEnvironmentVariable("DB_PORT") 
        : "5432";

    var dbName = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("POSTGRES_DB")) 
        ? Environment.GetEnvironmentVariable("POSTGRES_DB") 
        : "TableBookingDB";

    var dbUser = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("POSTGRES_USER")) 
        ? Environment.GetEnvironmentVariable("POSTGRES_USER") 
        : "TableBookingUser";

    var dbPassword = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")) 
        ? Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") 
        : "postgres";

    connectionString = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword}";
    
    o.UseNpgsql(connectionString);
});
builder.Services.AddHostedService<DbInitializerService>();
builder.Services.AddHealthChecks().AddCheck<DbHealthCheck>(
        nameof(DbHealthCheck),
        failureStatus: HealthStatus.Unhealthy);

builder.Services.AddIdentity<AppUser, AppRole>(x =>
{
    x.Password.RequireDigit = false;
    x.Password.RequiredLength = 2;
    x.Password.RequireUppercase = false;
    x.Password.RequireLowercase = false;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequiredUniqueChars = 0;
    x.Lockout.AllowedForNewUsers = true;
    x.Lockout.MaxFailedAccessAttempts = 5;
    x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
    x.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<TableBookingContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"] ?? string.Empty))
    };
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
    options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<ITableConverter, TableConverter>();
builder.Services.AddTransient<ITableToGetConverter, TableToGetConverter>();
builder.Services.AddTransient<IRatingConverter, RatingConverter>();
builder.Services.AddTransient<IShortUserInfoConverter, ShortUserInfoConverter>();

builder.Services.AddTransient<IBookingService, BookingService>();
builder.Services.AddTransient<IRestaurantService, RestaurantService>();
builder.Services.AddTransient<ITableService, TableService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRatingService, RatingService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    await RolesExtension.SeedRolesAsync(serviceProvider);
}

app.UseMiddleware<TokenRevocationMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.MapHealthChecks("/healthz").RequireAuthorization();
app.UseCors("cors");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program;