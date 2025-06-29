using Microsoft.AspNetCore.Mvc;
using VideoGameCatalog.API.Application.Commands;
using VideoGameCatalog.API.Application.Queries;
using VideoGameCatalog.API.DTOs;

namespace VideoGameCatalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoGamesController : ControllerBase
{
    private readonly AddVideoGameCommand _addCommand;
    private readonly UpdateVideoGameCommand _updateCommand;
    private readonly DeleteVideoGameCommand _deleteCommand;
    private readonly GetAllVideoGamesQuery _getAllQuery;
    private readonly GetVideoGameByIdQuery _getByIdQuery;

    public VideoGamesController(
        AddVideoGameCommand addCommand,
        UpdateVideoGameCommand updateCommand,
        DeleteVideoGameCommand deleteCommand,
        GetAllVideoGamesQuery getAllQuery,
        GetVideoGameByIdQuery getByIdQuery)
    {
        _addCommand = addCommand;
        _updateCommand = updateCommand;
        _deleteCommand = deleteCommand;
        _getAllQuery = getAllQuery;
        _getByIdQuery = getByIdQuery;
    }

    [HttpGet]
    public async Task<ActionResult<List<VideoGameDto>>> GetAll()
    {
        var games = await _getAllQuery.ExecuteAsync();
        return Ok(games);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VideoGameDto>> GetById(int id)
    {
        var game = await _getByIdQuery.ExecuteAsync(id);
        return game is not null ? Ok(game) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateVideoGameDto dto)
    {
        var id = await _addCommand.ExecuteAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateVideoGameDto dto)
    {
        if (id != dto.Id)
            return BadRequest("ID mismatch");

        var result = await _updateCommand.ExecuteAsync(dto);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _deleteCommand.ExecuteAsync(id);
        return result ? NoContent() : NotFound();
    }
}
