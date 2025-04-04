namespace TableBooking.Model.Models;

public class OpeningAndClosingHours
{
    public WeekDayInfo Monday { get; set; } = new();
    public WeekDayInfo Tuesday { get; set; } = new();
    public WeekDayInfo Wednesday { get; set; } = new();
    public WeekDayInfo Thursday { get; set; } = new();
    public WeekDayInfo Friday { get; set; } = new();
    public WeekDayInfo Saturday { get; set; } = new();
    public WeekDayInfo Sunday { get; set; } = new();
}

public class WeekDayInfo
{
    public bool Closed { get; set; }
    public TimeSpan? OpenTime { get; set; }
    public TimeSpan? CloseTime { get; set; }
}