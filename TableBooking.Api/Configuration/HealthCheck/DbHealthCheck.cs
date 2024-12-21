using Microsoft.Extensions.Diagnostics.HealthChecks;
using TableBooking.Model;
using Microsoft.EntityFrameworkCore;

namespace TableBooking.Api.Configuration.HealthCheck
{
    public class DbHealthCheck : IHealthCheck
    {
        private readonly TableBookingContext _tableBookingContext;
        public DbHealthCheck(TableBookingContext tableBookingContext)
        {
            _tableBookingContext = tableBookingContext;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var tables = await _tableBookingContext.Tables.ToListAsync();
                if (tables.Count > 0)
                    return HealthCheckResult.Healthy("Database is healthy and seeded.");
                return HealthCheckResult.Unhealthy("Database is healthy, but no seeded data is found.");
            }
            catch
            {
                return HealthCheckResult.Unhealthy("Database is not healthy or not seeded.");
            }
        }
    }
}


