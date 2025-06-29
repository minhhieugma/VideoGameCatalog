using VideoGameCatalog.API.Data;

namespace VideoGameCatalog.API.Application.Commands;

public class DeleteVideoGameCommand
{
    private readonly ApplicationDbContext _context;

    public DeleteVideoGameCommand(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExecuteAsync(int id)
    {
        var existing = await _context.VideoGames.FindAsync(id);
        if (existing == null)
            return false;

        _context.VideoGames.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
