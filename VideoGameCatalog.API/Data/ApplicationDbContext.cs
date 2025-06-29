using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.API.Models;

namespace VideoGameCatalog.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<VideoGame> VideoGames => Set<VideoGame>();
}