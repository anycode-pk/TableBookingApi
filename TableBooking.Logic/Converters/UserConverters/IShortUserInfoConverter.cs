using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableBooking.Model.Dtos.UserDtos;
using TableBooking.Model.Models;

namespace TableBooking.Logic.Converters.UserConverters
{
    public interface IShortUserInfoConverter
    {
        public UserShortInfoDto UserToUserShortInfo(AppUser user);
    }
}
