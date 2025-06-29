using System;
using FluentValidation.TestHelper;
using VideoGameCatalog.API.DTOs;
using VideoGameCatalog.API.Validation;
using Xunit;

namespace VideoGameCatalog.Tests.Validation;

public class UpdateVideoGameDtoValidatorTests
{
    private readonly UpdateVideoGameDtoValidator _validator = new();

    [Fact]
    public void Validator_Should_Reject_Missing_Id()
    {
        var dto = new UpdateVideoGameDto
        {
            Id = 0,
            Title = "Game",
            Genre = "Action",
            ReleaseDate = DateTime.Today
        };

        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Validator_Should_Reject_Empty_Title()
    {
        var dto = new UpdateVideoGameDto { Id = 1, Genre = "RPG", ReleaseDate = DateTime.Today };
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Validator_Should_Reject_Future_ReleaseDate()
    {
        var dto = new UpdateVideoGameDto
        {
            Id = 1,
            Title = "Game",
            Genre = "RPG",
            ReleaseDate = DateTime.Today.AddDays(5)
        };

        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.ReleaseDate);
    }

    [Fact]
    public void Validator_Should_Accept_Valid_Dto()
    {
        var dto = new UpdateVideoGameDto
        {
            Id = 1,
            Title = "Updated Game",
            Genre = "RPG",
            ReleaseDate = DateTime.Today
        };

        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveAnyValidationErrors();
    }
}