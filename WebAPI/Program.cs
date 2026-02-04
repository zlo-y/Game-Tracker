using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Infrastructure;
using Application.Common.Interfaces;
using WebAPI.Middlewares;
using Application.Common;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 
// Добавление сервисов для дальнейшей работы с ними!
// 
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddControllers();
builder.Services.AddScoped<IApplicationDbContext>(provider => 
    provider.GetRequiredService<AppDbContext>());
builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
}

app.UseHttpsRedirection();


app.MapControllers();


app.Run();

