using VideoGameCatalog.API.Models;

namespace VideoGameCatalog.API.Data;

public static class DbSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        try
        {
            // Ensure the database is created (useful with SQLite)
            context.Database.EnsureCreated();

            // Skip seeding if already populated
            if (context.VideoGames.Any())
                return;

            var games = new List<VideoGame>
            {
                new() { Title = "The Legend of Zelda: Breath of the Wild", Genre = "Adventure", ReleaseDate = new DateTime(2017, 3, 3) },
                new() { Title = "Halo Infinite", Genre = "Shooter", ReleaseDate = new DateTime(2021, 12, 8) },
                new() { Title = "God of War", Genre = "Action", ReleaseDate = new DateTime(2018, 4, 20) },
                new() { Title = "Elden Ring", Genre = "RPG", ReleaseDate = new DateTime(2022, 2, 25) },
                new() { Title = "Minecraft", Genre = "Sandbox", ReleaseDate = new DateTime(2011, 11, 18) },
                new() { Title = "Stardew Valley", Genre = "Simulation", ReleaseDate = new DateTime(2016, 2, 26) },
                new() { Title = "Cyberpunk 2077", Genre = "RPG", ReleaseDate = new DateTime(2020, 12, 10) },
                new() { Title = "The Witcher 3", Genre = "RPG", ReleaseDate = new DateTime(2015, 5, 19) },
                new() { Title = "Grand Theft Auto V", Genre = "Action", ReleaseDate = new DateTime(2013, 9, 17) },
                new() { Title = "Super Mario Odyssey", Genre = "Platformer", ReleaseDate = new DateTime(2017, 10, 27) },
                new() { Title = "Red Dead Redemption 2", Genre = "Action", ReleaseDate = new DateTime(2018, 10, 26) },
                new() { Title = "Hades", Genre = "Roguelike", ReleaseDate = new DateTime(2020, 9, 17) },
                new() { Title = "Celeste", Genre = "Platformer", ReleaseDate = new DateTime(2018, 1, 25) },
                new() { Title = "Tunic", Genre = "Adventure", ReleaseDate = new DateTime(2022, 3, 16) },
                new() { Title = "Slay the Spire", Genre = "Card Game", ReleaseDate = new DateTime(2019, 1, 23) },
                new() { Title = "Fortnite", Genre = "Battle Royale", ReleaseDate = new DateTime(2017, 7, 21) },
                new() { Title = "Valorant", Genre = "Shooter", ReleaseDate = new DateTime(2020, 6, 2) },
                new() { Title = "Overwatch", Genre = "Shooter", ReleaseDate = new DateTime(2016, 5, 24) },
                new() { Title = "Apex Legends", Genre = "Battle Royale", ReleaseDate = new DateTime(2019, 2, 4) },
                new() { Title = "Terraria", Genre = "Sandbox", ReleaseDate = new DateTime(2011, 5, 16) }
            };

            context.VideoGames.AddRange(games);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Seeding Failed]: {ex.Message}");
        }
    }
}
