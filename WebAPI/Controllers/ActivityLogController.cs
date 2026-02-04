using Application.Activities.Commands;
using Domain;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivityLogController : ControllerBase
{
    private readonly IMediator _mediator;
// 
// Контроллер теперь не лезет в базу, он просто "секретарь"
// 
    public ActivityLogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("start")]
    public async Task<ActionResult<Guid>> StartActivity([FromBody] string activityName)
    {
// 
// Просто перекладываем ответственность на Mediator. 
// Если логика создания изменится, мы поменяем Handler, а этот метод не тронем.
// 

       var resultId = await _mediator.Send(new CreateActivityCommand(activityName));

       return Ok(resultId);
    }
}