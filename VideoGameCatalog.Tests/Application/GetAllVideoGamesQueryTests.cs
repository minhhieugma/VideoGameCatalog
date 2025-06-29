using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.API.Application.Queries;
using VideoGameCatalog.API.Data;
using VideoGameCatalog.API.DTOs;
using VideoGameCatalog.API.Models;
using Xunit;

namespace VideoGameCatalog.Tests.Application;

public class GetAllVideoGamesQueryTests
{
    private ApplicationDbContext GetSeededDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // fresh DB for isolation
            .Options;

        var context = new ApplicationDbContext(options);
        DbSeeder.Seed(context); // use your real seeder
        return context;
    }

    [Fact]
    public async Task ExecuteAsync_Returns_Seeded_Games_As_Dtos()
    {
        // Arrange
        var context = GetSeededDbContext();
        var query = new GetAllVideoGamesQuery(context);

        // Act
        var result = await query.ExecuteAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.True(result.Count >= 20); // based on your seed data count
        Assert.Contains(result, v => v.Title == "Elden Ring");
        Assert.Contains(result, v => v.Genre == "RPG");
        Assert.All(result, item => Assert.IsType<VideoGameDto>(item));
    }

    [Fact]
    public async Task ExecuteAsync_Contains_Specific_Known_Titles()
    {
        var context = GetSeededDbContext();
        var query = new GetAllVideoGamesQuery(context);

        var result = await query.ExecuteAsync();
        var titles = result.Select(r => r.Title).ToList();

        Assert.Contains("Minecraft", titles);
        Assert.Contains("God of War", titles);
        Assert.Contains("Halo Infinite", titles);
    }

    [Fact]
    public async Task ExecuteAsync_Does_Not_Include_Nonexistent_Games()
    {
        var context = GetSeededDbContext();
        var query = new GetAllVideoGamesQuery(context);

        var result = await query.ExecuteAsync();

        Assert.DoesNotContain(result, r => r.Title == "Half-Life 3");
        Assert.DoesNotContain(result, r => r.Genre == "MOBA");
    }
}
