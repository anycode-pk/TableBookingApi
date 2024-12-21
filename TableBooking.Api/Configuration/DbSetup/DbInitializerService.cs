using TableBooking.Model;
using Microsoft.EntityFrameworkCore;

namespace TableBooking.Api.Configuration.DbSetup
{
    public class DbInitializerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DbInitializerService> _logger;
        public DbInitializerService(ILogger<DbInitializerService> logger, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<TableBookingContext>();
                if ((await dbContext.Database.GetPendingMigrationsAsync(stoppingToken)).Any())
                {
                    await dbContext.Database.MigrateAsync(cancellationToken: stoppingToken);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Failed within migration {Msg}: ", e);
            }

        }
    }
}
