using Microsoft.AspNetCore.Http;
using Application.Common.Interfaces;
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
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
            return userIdClaim != null ? Guid.Parse(userIdClaim) : Guid.Empty;
        }
    }
}