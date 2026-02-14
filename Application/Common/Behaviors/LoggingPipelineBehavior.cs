using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.Common.Behaviors;

// 
// Пайплайн для логирования запросов и ответов MediatR
// 
public class LoggingPipelineBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>
where TRequest : notnull
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;
    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle (TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var stopwatch = Stopwatch.StartNew();
        _logger.LogInformation(">>> [START] Handling {RequestName} | Data: {@Request}", requestName, request);

// 
// Следующий делегат в пайплайне будет вызван внутри блока try-catch для отслеживания ошибок и времени выполнения
// 
        try
        {
            var response = await next();
            stopwatch.Stop();
            _logger.LogInformation("<<< [END] Handled {RequestName} | Execution Time: {Elapsed}ms", requestName, stopwatch.ElapsedMilliseconds);
            return response;

        }
        catch(Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex, "!!! [ERROR] Failed {RequestName} after {Elapsed}ms", requestName, stopwatch.ElapsedMilliseconds);
            throw;
        }
    }
}