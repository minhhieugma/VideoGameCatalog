using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.API.Data;
using VideoGameCatalog.API.DTOs;

namespace VideoGameCatalog.API.Application.Queries;

public class GetVideoGameByIdQuery
{
    private readonly ApplicationDbContext _context;

    public GetVideoGameByIdQuery(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<VideoGameDto?> ExecuteAsync(int id)
    {
        return await _context.VideoGames
            .AsNoTracking()
            .Where(v => v.Id == id)
            .Select(v => new VideoGameDto
            {
                Id = v.Id,
                Title = v.Title,
                Genre = v.Genre,
                ReleaseDate = v.ReleaseDate
            })
            .FirstOrDefaultAsync();
        
        // return await _context.VideoGames
        //     .Where(v => v.Id == id)
        //     .Select(v => new VideoGameDto
        //     {
        //         Id = v.Id,
        //         Title = v.Title,
        //         Genre = v.Genre,
        //         ReleaseDate = v.ReleaseDate
        //     })
        //     .FirstOrDefaultAsync();
    }
}