namespace TableBooking.Logic.Converters.RestaurantConverters;

using Model.Dtos.RestaurantDtos;
using Model.Models;

public class RestaurantConverter : IRestaurantConverter
{
    public OpeningAndClosingHoursDto ConvertToDto(OpeningAndClosingHours hours)
    {
        return new OpeningAndClosingHoursDto
        {
            MondayHours = FormatTimeSpan(hours.MondayOpen, hours.MondayClose),
            TuesdayHours = FormatTimeSpan(hours.TuesdayOpen, hours.TuesdayClose),
            WednesdayHours = FormatTimeSpan(hours.WednesdayOpen, hours.WednesdayClose),
            ThursdayHours = FormatTimeSpan(hours.ThursdayOpen, hours.ThursdayClose),
            FridayHours = FormatTimeSpan(hours.FridayOpen, hours.FridayClose),
            SaturdayHours = FormatTimeSpan(hours.SaturdayOpen, hours.SaturdayClose),
            SundayHours = FormatTimeSpan(hours.SundayOpen, hours.SundayClose)
        };
    }

    public OpeningAndClosingHours ConvertToModel(OpeningAndClosingHoursDto hoursDto)
    {
        return new OpeningAndClosingHours
        {
            MondayOpen = ParseTime(hoursDto.MondayHours, true),
            MondayClose = ParseTime(hoursDto.MondayHours, false),
            
            TuesdayOpen = ParseTime(hoursDto.TuesdayHours, true),
            TuesdayClose = ParseTime(hoursDto.TuesdayHours, false),
            
            WednesdayOpen = ParseTime(hoursDto.WednesdayHours, true),
            WednesdayClose = ParseTime(hoursDto.WednesdayHours, false),
            
            ThursdayOpen = ParseTime(hoursDto.ThursdayHours, true),
            ThursdayClose = ParseTime(hoursDto.ThursdayHours, false),
            
            FridayOpen = ParseTime(hoursDto.FridayHours, true),
            FridayClose = ParseTime(hoursDto.FridayHours, false),
            
            SaturdayOpen = ParseTime(hoursDto.SaturdayHours, true),
            SaturdayClose = ParseTime(hoursDto.SaturdayHours, false),
            
            SundayOpen = ParseTime(hoursDto.SundayHours, true),
            SundayClose = ParseTime(hoursDto.SundayHours, false)
        };
    }

    private static string FormatTimeSpan(TimeSpan? open, TimeSpan? close)
    {
        if (open == null || close == null)
        {
            return "closed";
        }

        return $"{open:hh\\:mm}-{close:hh\\:mm}";
    }

    private static TimeSpan? ParseTime(string hours, bool isOpen)
    {
        if (string.IsNullOrEmpty(hours) || hours.Equals("closed", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        var times = hours.Split('-');
        if (times.Length != 2) return null;

        if (isOpen)
        {
            return TimeSpan.TryParse(times[0], out var openTime) ? openTime : null;
        }

        return TimeSpan.TryParse(times[1], out var closeTime) ? closeTime : null;
    }
}