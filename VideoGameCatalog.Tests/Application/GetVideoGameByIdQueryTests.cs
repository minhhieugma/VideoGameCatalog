using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.API.Application.Queries;
using VideoGameCatalog.API.Data;
using VideoGameCatalog.API.Models;
using Xunit;

namespace VideoGameCatalog.Tests.Application;

public class GetVideoGameByIdQueryTests
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task ExecuteAsync_Returns_Correct_GameDto_When_Found()
    {
        var context = GetDbContext();
        var game = new VideoGame
        {
            Title = "Celeste",
            Genre = "Platformer",
            ReleaseDate = new DateTime(2018, 1, 25)
        };
        context.VideoGames.Add(game);
        await context.SaveChangesAsync();

        var query = new GetVideoGameByIdQuery(context);
        var result = await query.ExecuteAsync(game.Id);

        Assert.NotNull(result);
        Assert.Equal("Celeste", result!.Title);
    }

    [Fact]
    public async Task ExecuteAsync_Returns_Null_When_NotFound()
    {
        var context = GetDbContext();
        var query = new GetVideoGameByIdQuery(context);

        var result = await query.ExecuteAsync(999);
        Assert.Null(result);
    }
}