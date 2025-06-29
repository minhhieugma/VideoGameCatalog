using Application.VideoGames.Commands;
using Application.VideoGames.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("video-games")]
public class VideoGamesController : ControllerBase
{
    private readonly IMediator _mediator;

    public VideoGamesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<GetAllVideoGamesQuery.Result>> Get(CancellationToken cancellationToken)
    {
        IEnumerable<GetAllVideoGamesQuery.Result> response =
            await _mediator.Send(new GetAllVideoGamesQuery(), cancellationToken);

        return response;
    }

    [HttpGet("{videoGameId}")]
    public async Task<GetVideoGameDetailByIdQuery.Result?> Get(int videoGameId, CancellationToken cancellationToken)
    {
        GetVideoGameDetailByIdQuery.Result? response = await _mediator.Send(new GetVideoGameDetailByIdQuery
            { VideoGameId = videoGameId }, cancellationToken);

        return response;
    }

    [HttpPost]
    public async Task<AddVideoGameCommand.Result> AddVideoGame([FromBody] AddVideoGameCommand command,
        CancellationToken cancellationToken)
    {
        AddVideoGameCommand.Result result =
            await _mediator.Send(command, cancellationToken);

        return result;
    }

    [HttpDelete("{videoGameId}")]
    public async Task<DeleteVideoGameCommand.Result> DeleteVideoGame(int videoGameId,
        CancellationToken cancellationToken)
    {
        DeleteVideoGameCommand.Result result =
            await _mediator.Send(new DeleteVideoGameCommand { VideoGameId = videoGameId }, cancellationToken);

        return result;
    }


    [HttpPatch("{videoGameId}")]
    public async Task<UpdateVideoGameCommand.Result> UpdateVideoGame(int videoGameId,
        [FromBody] UpdateVideoGameRequest request, CancellationToken cancellationToken)
    {
        UpdateVideoGameCommand.Result result =
            await _mediator.Send(new UpdateVideoGameCommand
            {
                VideoGameId = videoGameId,
                Title = request.Title,
                Genre = request.Genre,
                ReleaseDate = request.ReleaseDate
            }, cancellationToken);

        return result;
    }

    public sealed class UpdateVideoGameRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
    }
}