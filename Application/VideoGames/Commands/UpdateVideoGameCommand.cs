using Domain.VideoGames;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.VideoGames.Commands;

public class UpdateVideoGameCommand : IRequest<UpdateVideoGameCommand.Result>
{
    public int VideoGameId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }

    public class Validator : AbstractValidator<UpdateVideoGameCommand>
    {
        public Validator()
        {
            RuleFor(p => p.VideoGameId).GreaterThan(0);
        }
    }

    public class Handler : IRequestHandler<UpdateVideoGameCommand, Result>
    {
        private readonly ApplicationDbContext _context;

        public Handler(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateVideoGameCommand command, CancellationToken cancellationToken)
        {
            VideoGame? existing = await _context.VideoGames
                .SingleOrDefaultAsync(p => p.VideoGameId == command.VideoGameId, cancellationToken);
            if (existing == null)
                return new Result();

            existing.Title = command.Title;
            existing.Genre = command.Genre;
            existing.ReleaseDate = command.ReleaseDate;

            await _context.SaveChangesAsync(cancellationToken);

            return new Result
            {
                VideoGameId = existing.VideoGameId
            };
        }
    }

    public sealed class Result
    {
        public int VideoGameId { get; set; }
    }
}