using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.API.Application.Commands;
using VideoGameCatalog.API.Data;
using VideoGameCatalog.API.Models;
using Xunit;

namespace VideoGameCatalog.Tests.Application;

public class DeleteVideoGameCommandTests
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task ExecuteAsync_Deletes_Existing_Game()
    {
        var context = GetDbContext();
        var game = new VideoGame { Title = "Test", Genre = "Action", ReleaseDate = DateTime.Today };
        context.VideoGames.Add(game);
        await context.SaveChangesAsync();

        var command = new DeleteVideoGameCommand(context);
        var result = await command.ExecuteAsync(game.Id);

        Assert.True(result);
        Assert.Null(await context.VideoGames.FindAsync(game.Id));
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsFalse_If_NotFound()
    {
        var context = GetDbContext();
        var command = new DeleteVideoGameCommand(context);

        var result = await command.ExecuteAsync(999); // non-existent
        Assert.False(result);
    }
}