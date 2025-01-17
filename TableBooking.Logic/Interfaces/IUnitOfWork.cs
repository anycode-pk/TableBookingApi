namespace TableBooking.Logic.Interfaces;

public interface IUnitOfWork
{
    public IBookingRepository BookingRepository { get; }
    public IRestaurantRepository RestaurantRepository { get; }
    public ITableRepository TableRepository { get; }
    public IUserRepository UserRepository { get; }
    public IRatingRepository RatingRepository { get; }
    void Dispose();
    Task SaveChangesAsync();
}