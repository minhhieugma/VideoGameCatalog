using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.API.Data;
using VideoGameCatalog.API.DTOs;
using VideoGameCatalog.API.Models;

namespace VideoGameCatalog.API.Application.Commands;

public class UpdateVideoGameCommand
{
    private readonly ApplicationDbContext _context;

    public UpdateVideoGameCommand(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExecuteAsync(UpdateVideoGameDto dto)
    {
        var existing = await _context.VideoGames.FindAsync(dto.Id);
        if (existing == null)
            return false;

        existing.Title = dto.Title;
        existing.Genre = dto.Genre;
        existing.ReleaseDate = dto.ReleaseDate;

        _context.VideoGames.Update(existing);
        await _context.SaveChangesAsync();

        return true;
    }
}