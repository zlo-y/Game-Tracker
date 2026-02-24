using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Common.Behaviors;

// 
// Класс-универсал(посредник) между пользователем и DB(БД)!
// 
public class CachingBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>, ICacheble
{
    private readonly IDistributedCache _cache;

    public CachingBehavior(IDistributedCache cache)
    {
        _cache = cache;
    }

// 
// Основной метод перехвата запроса.
// 
    public async Task<TResponse> Handle (TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
// 
// Получаем данные из Redis по уникальному ключу запроса
// 
        var cachedResponse = await _cache.GetStringAsync(request.CacheKey, cancellationToken);

        if(cachedResponse != null)
        { 
            return JsonSerializer.Deserialize<TResponse>(cachedResponse)!;
        }

        var response = await next();

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = request.Expiration ?? TimeSpan.FromMinutes(5)
        };

        await _cache.SetStringAsync(request.CacheKey , JsonSerializer.Serialize(response), options , cancellationToken);
        return response;
    }
}
