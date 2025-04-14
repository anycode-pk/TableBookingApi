namespace TableBooking.Logic.Settings;

public sealed record BookingSettings
{
    public int DefaultDurationOfBooking { get; init; }
    public int DefaultCountOfReturnedSuggestions { get; init; }
}