namespace TableBooking.Api.Validators;

using System;
using System.Linq.Expressions;
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

        ValidateWeekDay(x => x.OpeningAndClosingHours.Monday, "Monday");
        ValidateWeekDay(x => x.OpeningAndClosingHours.Tuesday, "Tuesday");
        ValidateWeekDay(x => x.OpeningAndClosingHours.Wednesday, "Wednesday");
        ValidateWeekDay(x => x.OpeningAndClosingHours.Thursday, "Thursday");
        ValidateWeekDay(x => x.OpeningAndClosingHours.Friday, "Friday");
        ValidateWeekDay(x => x.OpeningAndClosingHours.Saturday, "Saturday");
        ValidateWeekDay(x => x.OpeningAndClosingHours.Sunday, "Sunday");

    }

    private void ValidateWeekDay(Expression<Func<RestaurantShortInfoDto, WeekDayInfo>> dayExpression, string dayName)
    {
        RuleFor(dayExpression)
            .NotNull().WithMessage($"{dayName} information is required.")
            .DependentRules(() =>
            {
                RuleFor(dayExpression)
                    .Must(day => day.Closed || (day.OpenTime.HasValue && day.CloseTime.HasValue))
                    .WithMessage($"{dayName} must have opening and closing hours unless it is marked as closed.");

                RuleFor(dayExpression)
                    .Must(day => !day.OpenTime.HasValue || !day.CloseTime.HasValue || day.OpenTime < day.CloseTime)
                    .WithMessage($"{dayName} closing time must be after opening time.");
            });
    }
}
