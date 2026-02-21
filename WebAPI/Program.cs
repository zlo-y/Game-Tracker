using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Infrastructure;
using Application.Common.Interfaces;
using WebAPI.Middlewares;
using Application.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using WebAPI.Extensions;



// 
// Создание билдера приложения 
// 
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


// 
// Добавление сервисов для дальнейшей работы с ними!
// 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddControllers();
builder.Services.AddScoped<IApplicationDbContext>(provider => 
    provider.GetRequiredService<AppDbContext>());
builder.Services.AddApplicationServices();
builder.Services.AddIdentity<Domain.User, IdentityRole<Guid>>(options => 
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// 
// Добавление аутентификации с использованием JWT
// 
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT_ISSUER"],
        ValidAudience = builder.Configuration["JWT_AUDIENCE"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT_KEY"]!))
    };
});


// 
// Настройка конвейера обработки запросов
// 
var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();


app.Run();

