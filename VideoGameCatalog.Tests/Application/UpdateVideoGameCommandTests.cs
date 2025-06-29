using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.API.Application.Commands;
using VideoGameCatalog.API.Data;
using VideoGameCatalog.API.DTOs;
using VideoGameCatalog.API.Models;
using Xunit;

namespace VideoGameCatalog.Tests.Application;

public class UpdateVideoGameCommandTests
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task ExecuteAsync_Updates_Game_Successfully()
    {
        var context = GetDbContext();
        var original = new VideoGame
        {
            Title = "Original",
            Genre = "RPG",
            ReleaseDate = new DateTime(2000, 5, 1)
        };
        context.VideoGames.Add(original);
        await context.SaveChangesAsync();

        // Detach original to simulate real API behavior
        context.Entry(original).State = EntityState.Detached;

        var dto = new UpdateVideoGameDto
        {
            Id = original.Id,
            Title = "Updated",
            Genre = "RPG",
            ReleaseDate = new DateTime(2001, 1, 1)
        };

        var command = new UpdateVideoGameCommand(context);
        var result = await command.ExecuteAsync(dto);

        var updated = await context.VideoGames.FindAsync(original.Id);
        Assert.True(result);
        Assert.Equal("Updated", updated!.Title);
        Assert.Equal(new DateTime(2001, 1, 1), updated.ReleaseDate);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsFalse_When_Game_NotFound()
    {
        var context = GetDbContext();
        var command = new UpdateVideoGameCommand(context);

        var fakeDto = new UpdateVideoGameDto
        {
            Id = 999,
            Title = "Non-existent",
            Genre = "Strategy",
            ReleaseDate = DateTime.Today
        };

        var result = await command.ExecuteAsync(fakeDto);
        Assert.False(result);
    }
}
