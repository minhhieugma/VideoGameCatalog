using FluentValidation;
using VideoGameCatalog.API.DTOs;

namespace VideoGameCatalog.API.Validation;

public class UpdateVideoGameDtoValidator : AbstractValidator<UpdateVideoGameDto>
{
    public UpdateVideoGameDtoValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");

        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(v => v.Genre)
            .NotEmpty().WithMessage("Genre is required.");

        RuleFor(v => v.ReleaseDate)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("Release date cannot be in the future.");
    }
}