using TableBooking.Model.Dtos.UserDtos;
using TableBooking.Model.Models;

namespace TableBooking.Logic.Converters.UserConverters
{
    public class ShortUserInfoConverter : IShortUserInfoConverter
    {
        public UserShortInfoDto UserToUserShortInfo(AppUser user)
        {
            return new UserShortInfoDto
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }
    }
}
