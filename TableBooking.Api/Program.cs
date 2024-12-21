using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TableBooking.Model;
using Serilog;
using TableBooking.Logic.Interfaces;
using TableBooking.Logic;
using TableBooking.Api.Services;
using TableBooking.Api.Configuration.DbSetup;
using TableBooking.Api.Configuration.HealthCheck;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TableBooking.Api.Interfaces;
using TableBooking.Model.Models;
using TableBooking.Logic.Converters.TableConverters;
using TableBooking.Logic.Converters.RatingConverters;
using TableBooking.Logic.Converters.UserConverters;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
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
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
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

    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
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
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
    options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<ITableConverter, TableConverter>(); // doczytaj debilu
builder.Services.AddTransient<ITableToGetConverter, TableToGetConverter>();
builder.Services.AddTransient<IRatingConverter, RatingConverter>();
builder.Services.AddTransient<IShortUserInfoConverter, ShortUserInfoConverter>();

builder.Services.AddTransient<IBookingService, BookingService>();
builder.Services.AddTransient<IRestaurantService, RestaurantService>();
builder.Services.AddTransient<ITableService, TableService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRatingService, RatingService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseSerilogRequestLogging();
app.MapHealthChecks("/healthz"); //.RequireHost("*:5001").RequireAuthorization();
app.UseCors("cors");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }