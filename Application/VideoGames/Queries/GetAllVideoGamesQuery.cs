﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.VideoGames.Queries;

public class GetAllVideoGamesQuery : IRequest<IEnumerable<GetAllVideoGamesQuery.Result>>
{
    public class Handler : IRequestHandler<GetAllVideoGamesQuery, IEnumerable<Result>>
    {
        private readonly ApplicationDbContext _context;

        public Handler(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Result>> Handle(GetAllVideoGamesQuery request,
            CancellationToken cancellationToken)
        {
            List<Result> videoGames = await _context.VideoGames
                .Select(v => new Result
                {
                    VideoGameId = v.VideoGameId,
                    Title = v.Title,
                    Genre = v.Genre,
                    ReleaseDate = v.ReleaseDate
                })
                .ToListAsync(cancellationToken);

            return videoGames;
        }
    }

    public record Result
    {
        public int VideoGameId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
    }
}