using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.API.Application.Commands;
using VideoGameCatalog.API.Data;
using VideoGameCatalog.API.DTOs;
using Xunit;

namespace VideoGameCatalog.Tests.Application;

public class AddVideoGameCommandTests
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task ExecuteAsync_Adds_Game_Successfully()
    {
        var context = GetDbContext();
        var command = new AddVideoGameCommand(context);

        var dto = new CreateVideoGameDto
        {
            Title = "Test Game",
            Genre = "Action",
            ReleaseDate = new DateTime(2022, 1, 1)
        };

        var id = await command.ExecuteAsync(dto);

        var added = await context.VideoGames.FindAsync(id);
        Assert.NotNull(added);
        Assert.Equal("Test Game", added!.Title);
    }
}