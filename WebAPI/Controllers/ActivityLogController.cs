using Application.Activities.Commands   ;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;




namespace WebAPI.Controllers;

[Authorize]
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
public async Task<ActionResult<Guid>> StartActivity([FromBody] StartActivityRequest request)
{

    var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    

    if (string.IsNullOrEmpty(userIdClaim)) 
        return Unauthorized("Не удалось определить пользователя");

    var userId = Guid.Parse(userIdClaim);

    var resultId = await _mediator.Send(new CreateActivityCommand(
        request.ActivityName, 
        request.GameId, 
        userId));

    return Ok(resultId);
}


}