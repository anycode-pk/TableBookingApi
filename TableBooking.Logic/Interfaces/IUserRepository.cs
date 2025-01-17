namespace TableBooking.Logic.Interfaces;

using Model.Models;

public interface IUserRepository : IGenericRepository<AppUser>
{
    public Task<IEnumerable<AppUser>> GetAllUsers();
    public Task<AppUser> GetUserById(Guid userId);
}