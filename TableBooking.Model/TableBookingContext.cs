namespace TableBooking.Model;

using Microsoft.EntityFrameworkCore;
using Models;

public class TableBookingContext : DbContext
{
    public TableBookingContext() { }
    public TableBookingContext(DbContextOptions<TableBookingContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Restaurant>(restaurantEntity =>
        {
            const string defaultImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/240px-No_image_available.svg.png";
                
            restaurantEntity.Property(r => r.PrimaryImageUrl).IsRequired()
                .HasDefaultValue(defaultImage).HasMaxLength(1000);

            restaurantEntity.Property(r => r.SecondaryImageUrl).IsRequired()
                .HasDefaultValue(defaultImage).HasMaxLength(1000);

            restaurantEntity.Property(r => r.Name).HasMaxLength(64);
            restaurantEntity.Property(r => r.Type).HasMaxLength(100);
            restaurantEntity.Property(r => r.Description).HasMaxLength(100);
            restaurantEntity.Property(r => r.Location).HasMaxLength(255);
            restaurantEntity.Property(r => r.Phone).HasMaxLength(32);
        });

        modelBuilder.Entity<Rating>(ratingEntity =>
        {
            ratingEntity.Property(r => r.Comment).HasMaxLength(500);
        });
            
        modelBuilder.Entity<AppUser>(appUserEntity =>
        {
            appUserEntity.Property(r => r.RefreshToken).HasMaxLength(512);
        });
        
        modelBuilder.Entity<RevokedToken>(appUserEntity =>
        {
            appUserEntity.Property(r => r.Token).HasMaxLength(512);
        });
            
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=TableBookingDB;Username=TableBookingUser;Password=postgres");
        }
    }

    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<AppUser> Users { get; set; }
    public DbSet<AppRole> Roles { get; set; }
    public DbSet<RevokedToken> RevokedTokens { get; set; }
}