namespace TableBooking.Logic.Converters.UserConverters;

using Model.Dtos.UserDtos;
using Model.Models;

public class ShortUserInfoConverter : IShortUserInfoConverter
{
    public UserShortInfoDto UserToUserShortInfo(AppUser user)
    {
        return new UserShortInfoDto
        {
            Id = user.Id,
            UserName = user.UserName ?? string.Empty
        };
    }
}