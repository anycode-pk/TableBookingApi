namespace TableBooking.Api.Validators;

using FluentValidation;
using Model.Dtos.RestaurantDtos;
using Model.Models;

public class RestaurantShortInfoDtoValidator : AbstractValidator<RestaurantShortInfoDto>
{
    public RestaurantShortInfoDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Type).NotEmpty().WithMessage("Type is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is required.");
        RuleFor(x => x.Location).NotEmpty().WithMessage("Location is required.");
        RuleFor(x => x.PrimaryImageURL).NotEmpty().WithMessage("Primary image URL is required.");
        RuleFor(x => x.SecondaryImageURL).NotEmpty().WithMessage("Secondary image URL is required.");
        RuleFor(x => x.Price).IsInEnum().WithMessage("Invalid price value.");
        RuleFor(x => x.OpeningAndClosingHours).NotNull().WithMessage("Opening and closing hours are required.");

        ValidateDay(x =>
        {
            var monday = x.OpeningAndClosingHours.Monday;
        }, "Monday");
        ValidateDay(x =>
        {
            var tuesday = x.OpeningAndClosingHours.Tuesday;
        }, "Tuesday");
        ValidateDay(x =>
        {
            var wednesday = x.OpeningAndClosingHours.Wednesday;
        }, "Wednesday");
        ValidateDay(x =>
        {
            var thursday = x.OpeningAndClosingHours.Thursday;
        }, "Thursday");
        ValidateDay(x =>
        {
            var friday = x.OpeningAndClosingHours.Friday;
        }, "Friday");
        ValidateDay(x =>
        {
            var saturday = x.OpeningAndClosingHours.Saturday;
        }, "Saturday");
        ValidateDay(x =>
        {
            var sunday = x.OpeningAndClosingHours.Sunday;
        }, "Sunday");
    }

    private void ValidateDay(Action<RestaurantShortInfoDto> dayExpression, string dayName)
    {
        RuleFor(x => x.OpeningAndClosingHours)
            .Must(openingHours =>
            {
                var day = openingHours.GetType().GetProperty(dayName)?.GetValue(openingHours);
                if (day == null) return true;

                var dayInfo = day as WeekDayInfo;
                if (dayInfo == null) return true;

                if (dayInfo.Closed) return !dayInfo.OpenTime.HasValue && !dayInfo.CloseTime.HasValue;

                return dayInfo.OpenTime.HasValue
                       && dayInfo.CloseTime.HasValue
                       && dayInfo.OpenTime < dayInfo.CloseTime;
            })
            .WithMessage($"{dayName} must have opening and closing hours unless it is marked as closed.");
    }
}