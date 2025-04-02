namespace TableBooking.Api.Validators;

using System;
using System.Linq.Expressions;
using FluentValidation;
using Model.Dtos.RestaurantDtos;

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

        ValidateTimeFormat(x => x.OpeningAndClosingHours.MondayOpen, "Monday open");
        ValidateTimeFormat(x => x.OpeningAndClosingHours.MondayClose, "Monday close");
        ValidateTimeFormat(x => x.OpeningAndClosingHours.TuesdayOpen, "Tuesday open");
        ValidateTimeFormat(x => x.OpeningAndClosingHours.TuesdayClose, "Tuesday close");
        ValidateTimeFormat(x => x.OpeningAndClosingHours.WednesdayOpen, "Wednesday open");
        ValidateTimeFormat(x => x.OpeningAndClosingHours.WednesdayClose, "Wednesday close");
        ValidateTimeFormat(x => x.OpeningAndClosingHours.ThursdayOpen, "Thursday open");
        ValidateTimeFormat(x => x.OpeningAndClosingHours.ThursdayClose, "Thursday close");
        ValidateTimeFormat(x => x.OpeningAndClosingHours.FridayOpen, "Friday open");
        ValidateTimeFormat(x => x.OpeningAndClosingHours.FridayClose, "Friday close");
        ValidateTimeFormat(x => x.OpeningAndClosingHours.SaturdayOpen, "Saturday open");
        ValidateTimeFormat(x => x.OpeningAndClosingHours.SaturdayClose, "Saturday close");
        ValidateTimeFormat(x => x.OpeningAndClosingHours.SundayOpen, "Sunday open");
        ValidateTimeFormat(x => x.OpeningAndClosingHours.SundayClose, "Sunday close");
    }

    private void ValidateTimeFormat(Expression<Func<RestaurantShortInfoDto, TimeSpan?>> timeExpression, string fieldName)
    {
        RuleFor(timeExpression)
            .Must(time => time == null || BeValidTimeSpan(time.Value))
            .WithMessage($"{fieldName} must be in the format HH:mm (e.g., 08:00) or null (closed).");
    }

    private bool BeValidTimeSpan(TimeSpan time)
    {
        return time >= TimeSpan.Zero && time < TimeSpan.FromHours(24);
    }
}
