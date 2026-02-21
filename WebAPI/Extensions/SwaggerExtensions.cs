using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace WebAPI.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Game Time Tracker API",
                Description = "Backend система для мониторинга игровой активности. Позволяет отслеживать время, анализировать статистику и управлять игровым процессом."
        });
// 
// Безопасность JWT
// 
      options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
           Name = "Authorization",
           Type = SecuritySchemeType.Http,
           Scheme = "Bearer",
           BearerFormat = "JWT",
           In = ParameterLocation.Header,
           Description = "Введите только JWT токен. Пример: 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'"
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

// 
// XML комментарии для методов API
// 
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }
        });

        return services;
    }
}