using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using FluentValidation;

namespace WebAPI.Middlewares;

// Middleware для глобальной обработки исключений в HTTP-запросах
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

// 
/// Точка входа в Middleware для обработки текущего HTTP-запроса!
// 
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
// Обработка всех необработанных исключений
        catch(Exception ex)
        {
            _logger.LogError(ex, "Произошла необработанная ошибка");
             context.Response.ContentType = "application/json";
             var response = ex switch
             {
                 FluentValidation.ValidationException validationException => new
                 {
                     StatusCode = (int)HttpStatusCode.BadRequest,
                     Title = "Ошибка валидации данных",
                     Message = "Один или несколько параметров не прошли проверку.",
                     Errors = (object)validationException.Errors.Select(
                        e => new
                        {
                            Field = e.PropertyName,
                            Error = e.ErrorMessage
                        }
                     )
                 },
                 _ => new
                 {
                     StatusCode = (int)StatusCodes.Status500InternalServerError,
                     Title = "Server Error",
                     Message = "Произошла непредвиденная ошибка на сервере.",
                     Errors = (object)Array.Empty<object>()
                 }
             };
             context.Response.StatusCode = (int)response.StatusCode;
             await context.Response.WriteAsJsonAsync(response);
        }
    }


// 
/// Метод для формирования и отправки унифицированного ответа в формате JSON.
// 
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
         var code = HttpStatusCode.InternalServerError;
         object result = new {error = "Произошла ошибка на сервере."};

         if(exception is ValidationException validationException)
         {
            code = HttpStatusCode.BadRequest;
            result = new
            {
                message = "Ошибка валидации данных",
                errors = validationException.Errors.Select(f => f.ErrorMessage)
            };
         }  

         context.Response.ContentType = "application/json";
         context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(JsonSerializer.Serialize(result));
    }
   
}