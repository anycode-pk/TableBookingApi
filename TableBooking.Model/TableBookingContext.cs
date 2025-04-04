namespace TableBooking.Model;

using Microsoft.EntityFrameworkCore;
using Models;

public class TableBookingContext : DbContext
{
    public TableBookingContext()
    {
    }

    public TableBookingContext(DbContextOptions<TableBookingContext> options) : base(options)
    {
    }

    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<AppUser> Users { get; set; }
    public DbSet<AppRole> Roles { get; set; }
    public DbSet<RevokedToken> RevokedTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Restaurant>(restaurantEntity =>
        {
            const string defaultImage =
                "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/240px-No_image_available.svg.png";

            restaurantEntity.Property(r => r.PrimaryImageUrl).IsRequired()
                .HasDefaultValue(defaultImage).HasMaxLength(1000);

            restaurantEntity.Property(r => r.SecondaryImageUrl).IsRequired()
                .HasDefaultValue(defaultImage).HasMaxLength(1000);

            restaurantEntity.Property(r => r.Name).HasMaxLength(64);
            restaurantEntity.Property(r => r.Type).HasMaxLength(100);
            restaurantEntity.Property(r => r.Description).HasMaxLength(100);
            restaurantEntity.Property(r => r.Location).HasMaxLength(255);
            restaurantEntity.Property(r => r.Phone).HasMaxLength(32);

            restaurantEntity.Property(r => r.Price)
                .IsRequired()
                .HasConversion<int>();

            restaurantEntity.OwnsOne(r => r.OpeningAndClosingHours, hours =>
            {
                hours.OwnsOne(h => h.Monday);
                hours.OwnsOne(h => h.Tuesday);
                hours.OwnsOne(h => h.Wednesday);
                hours.OwnsOne(h => h.Thursday);
                hours.OwnsOne(h => h.Friday);
                hours.OwnsOne(h => h.Saturday);
                hours.OwnsOne(h => h.Sunday);
            });
        });

        modelBuilder.Entity<Rating>(ratingEntity =>
        {
            ratingEntity.Property(r => r.Comment).HasMaxLength(500);
            ratingEntity.Property(r => r.DateOfRating).HasConversion(
                d => d.ToUniversalTime(),
                d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
        });

        modelBuilder.Entity<AppUser>(appUserEntity =>
        {
            appUserEntity.Property(r => r.RefreshToken).HasMaxLength(512);
            appUserEntity.Property(r => r.RefreshTokenExpiryTime)
                .HasMaxLength(512)
                .HasConversion(
                    d => d.HasValue ? d.Value.ToUniversalTime() : (DateTime?)null,
                    d => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null);
        });

        modelBuilder.Entity<RevokedToken>(appUserEntity =>
        {
            appUserEntity.Property(r => r.Token).HasMaxLength(512);
            appUserEntity.Property(r => r.RevokedAt).HasMaxLength(512).HasConversion(
                d => d.ToUniversalTime(),
                d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
        });

        modelBuilder.Entity<Booking>(appUserEntity =>
        {
            appUserEntity.Property(r => r.RestaurantId).IsRequired();
            appUserEntity.Property(r => r.TableId).IsRequired();
            appUserEntity.Property(r => r.AppUserId).IsRequired();
            appUserEntity.Property(r => r.AmountOfPeople).IsRequired();
            appUserEntity.Property(r => r.DurationInMinutes).IsRequired();
            appUserEntity.Property(r => r.Date).IsRequired().HasConversion(
                d => d.ToUniversalTime(),
                d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
        });

        modelBuilder.Entity<Table>(appUserEntity =>
        {
            appUserEntity.Property(t => t.RestaurantId).IsRequired();
            appUserEntity.Property(t => t.NumberOfSeats).IsRequired();
            appUserEntity.Property(t => t.Id).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5433;Database=TableBookingDB;Username=TableBookingUser;Password=postgres");
    }
}