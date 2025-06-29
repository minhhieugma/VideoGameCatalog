using System;
using FluentValidation.TestHelper;
using VideoGameCatalog.API.DTOs;
using VideoGameCatalog.API.Validation;
using Xunit;

namespace VideoGameCatalog.Tests.Validation;

public class CreateVideoGameDtoValidatorTests
{
    private readonly CreateVideoGameDtoValidator _validator = new();

    [Fact]
    public void Validator_Should_Reject_Empty_Title()
    {
        var dto = new CreateVideoGameDto { Genre = "RPG", ReleaseDate = DateTime.Today };
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Validator_Should_Reject_Future_ReleaseDate()
    {
        var dto = new CreateVideoGameDto { Title = "Test", Genre = "RPG", ReleaseDate = DateTime.Today.AddDays(1) };
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.ReleaseDate);
    }

    [Fact]
    public void Validator_Should_Accept_Valid_Dto()
    {
        var dto = new CreateVideoGameDto
        {
            Title = "Valid Game",
            Genre = "Adventure",
            ReleaseDate = DateTime.Today
        };
        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveAnyValidationErrors();
    }
}