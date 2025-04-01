namespace TableBooking.Logic.Converters.RestaurantConverters;

using Model.Dtos.RestaurantDtos;
using Model.Models;

public interface IRestaurantConverter
{
    OpeningAndClosingHoursDto ConvertToDto(OpeningAndClosingHours hours);
    OpeningAndClosingHours ConvertToModel(OpeningAndClosingHoursDto hoursDto);
}