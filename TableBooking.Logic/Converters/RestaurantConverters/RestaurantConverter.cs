namespace TableBooking.Logic.Converters.RestaurantConverters;

using Model.Dtos.RestaurantDtos;
using Model.Models;

public class RestaurantConverter : IRestaurantConverter
{
    private static readonly string[] DaysOfWeek =
        ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];

    public OpeningAndClosingHoursDto ConvertToDto(OpeningAndClosingHours hours)
    {
        var dto = new OpeningAndClosingHoursDto();
        foreach (var day in DaysOfWeek)
        {
            var property = typeof(OpeningAndClosingHours).GetProperty($"{day}Open");
            if (property == null) continue;

            var dayInfo = property.GetValue(hours) as WeekDayInfo;
            typeof(OpeningAndClosingHoursDto)
                .GetProperty($"{day}Hours")?
                .SetValue(dto, FormatTimeSpan(dayInfo));
        }

        return dto;
    }

    public OpeningAndClosingHours ConvertToModel(OpeningAndClosingHoursDto hoursDto)
    {
        var model = new OpeningAndClosingHours();
        foreach (var day in DaysOfWeek)
        {
            var dayHours = typeof(OpeningAndClosingHoursDto).GetProperty($"{day}Hours")?.GetValue(hoursDto) as string;
            var dayInfo = ParseTime(dayHours);

            typeof(OpeningAndClosingHours)
                .GetProperty($"{day}Open")?
                .SetValue(model, dayInfo);
        }

        return model;
    }

    private static string FormatTimeSpan(WeekDayInfo dayInfo)
    {
        if (dayInfo == null || dayInfo.Closed || dayInfo.OpenTime == null || dayInfo.CloseTime == null) return "closed";

        return $"{dayInfo.OpenTime:hh\\:mm}-{dayInfo.CloseTime:hh\\:mm}";
    }

    private static WeekDayInfo ParseTime(string hours)
    {
        if (string.IsNullOrEmpty(hours) || hours.Equals("closed", StringComparison.OrdinalIgnoreCase))
            return new WeekDayInfo { Closed = true };

        var times = hours.Split('-');
        if (times.Length != 2) return new WeekDayInfo { Closed = true };

        return new WeekDayInfo
        {
            Closed = false,
            OpenTime = TimeSpan.TryParse(times[0], out var openTime) ? openTime : null,
            CloseTime = TimeSpan.TryParse(times[1], out var closeTime) ? closeTime : null
        };
    }
}