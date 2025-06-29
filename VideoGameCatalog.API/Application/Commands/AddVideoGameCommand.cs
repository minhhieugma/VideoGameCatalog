using System.Threading.Tasks;
using VideoGameCatalog.API.Data;
using VideoGameCatalog.API.DTOs;
using VideoGameCatalog.API.Models;

namespace VideoGameCatalog.API.Application.Commands;

public class AddVideoGameCommand
{
    private readonly ApplicationDbContext _context;

    public AddVideoGameCommand(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> ExecuteAsync(CreateVideoGameDto dto)
    {
        var game = new VideoGame
        {
            Title = dto.Title,
            Genre = dto.Genre,
            ReleaseDate = dto.ReleaseDate
        };

        _context.VideoGames.Add(game);
        await _context.SaveChangesAsync();

        return game.Id;
    }
}