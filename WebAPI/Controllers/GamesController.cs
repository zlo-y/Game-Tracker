using Microsoft.AspNetCore;
using Domain;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Games.Queries;
using Application.Games.Commands;
using System.Net.Http.Headers;
using Application.Games.Delete;
using Application.Games.Put;
using WebAPI.Controllers.Models;

namespace WebAPI.Controllers;

// 
// Контроллер для WebAPI~
// 

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase{
    private readonly IMediator _mediator;

    public GamesController(IMediator mediator)
    {
        _mediator = mediator;
    }
// 
// Контроллер для получения результата!
// 
    [HttpGet]
    public async Task<ActionResult> GetGames([FromQuery] string? searchTeram , [FromQuery] string? genre)
    {
        var games = await _mediator.Send (new GetGamesQuery(searchTeram , genre));
        return Ok(games);  
    }
// 
// Контроллер для ввода данных!
// 
    [HttpPost]
    public async Task<ActionResult<Guid>> AddGame ([FromBody] AddGameRequest request) 
    {
        var id = await _mediator.Send(new AddGameCommand(request.Title , request.Genre));
        return Ok(id);
    }

// 
// Контроллер для удаления!
// 

    [HttpDelete("{id}")]
    public async Task<ActionResult<Guid>> DeleteGame(Guid id)
    {
        return Ok(await _mediator.Send(new DeleteGameCommand(id)));
    }
// 
// Контроллер для изменения!
// 

[HttpPut("{id}")]
public async Task<ActionResult<Guid>> UpdateGame(Guid id , [FromBody] string newTitle)
    {
        return Ok(await _mediator.Send(new PutGameCommand(id , newTitle)));
    }

}