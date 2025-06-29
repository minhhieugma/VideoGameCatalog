using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.API.Data;
using VideoGameCatalog.API.DTOs;

namespace VideoGameCatalog.API.Application.Queries;

public class GetAllVideoGamesQuery
{
    private readonly ApplicationDbContext _context;

    public GetAllVideoGamesQuery(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<VideoGameDto>> ExecuteAsync()
    {
        return await _context.VideoGames
            .AsNoTracking()
            .Select(v => new VideoGameDto
            {
                Id = v.Id,
                Title = v.Title,
                Genre = v.Genre,
                ReleaseDate = v.ReleaseDate
            })
            .ToListAsync();
        
        // return await _context.VideoGames
        //     .Select(v => new VideoGameDto
        //     {
        //         Id = v.Id,
        //         Title = v.Title,
        //         Genre = v.Genre,
        //         ReleaseDate = v.ReleaseDate
        //     })
        //     .ToListAsync();
    }
}