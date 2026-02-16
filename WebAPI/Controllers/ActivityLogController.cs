using Application.Activites.Command;
using Application.Activities.Commands   ;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;




namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ActivityLogController : ControllerBase
{
    private readonly IMediator _mediator;
// 
// Контроллер теперь не лезет в базу!
// 
    public ActivityLogController(IMediator mediator)
    {
        _mediator = mediator;
    }


// 
// Принимаем запрос на старт активности, извлекаем юзера из токена, отправляем команду в приложение и возвращаем ID новой активности.
// 
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


// 
// Осторожно! Чувствительно к ошибкам! Тут важно проверить, что активность существует, принадлежит юзеру и не завершена. 
// 
[HttpPost("stop/{id}")]
public async Task<ActionResult> StopActivity(Guid id)
{
   var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

   if(string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
   {
    return Unauthorized("Не удалось определить пользователя");
   }

   await _mediator.Send(new StopActivityCommand(id, userId));

   return Ok();
}
}