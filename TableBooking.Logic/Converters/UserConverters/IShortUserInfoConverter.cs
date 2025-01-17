namespace TableBooking.Logic.Converters.UserConverters;

using Model.Dtos.UserDtos;
using Model.Models;

public interface IShortUserInfoConverter
{
    public UserShortInfoDto UserToUserShortInfo(AppUser user);
}