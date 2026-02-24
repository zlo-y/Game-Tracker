using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;
public static class RedisExtensions
{
    public static IServiceCollection AddRedisCache (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
           options.Configuration = configuration.GetConnectionString("Redis");
           options.InstanceName = "GameTracker_"; 
        });
        return services;
    }
}