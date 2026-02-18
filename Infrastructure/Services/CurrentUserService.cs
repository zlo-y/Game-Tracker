using System.Security.Claims;
using Microsoft.AspNetCore.Http;

// 
// Сервис для получения информации о текущем пользователе из контекста HTTP-запроса,используется для извлечения UserId из JWT-токена.
// 
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var id = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return id != null ? Guid.Parse(id) : Guid.Empty;
            
        }
    }


}