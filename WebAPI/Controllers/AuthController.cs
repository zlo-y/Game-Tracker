using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Application.Common.DTOs;
using Application.Common.Interfaces;



namespace WebAPI.Controllers;



// 
// Контроллер для аутентификации и регистрации пользователей!
// 
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;
    public AuthController(UserManager<User> userManager, IConfiguration configuration, ITokenService tokenService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _tokenService = tokenService;
    }

// 
// Контроллер для регистрации пользователя!
// 
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto request)
    {
        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            DisplayName = request.DisplayName,
            CreatedAt = DateTime.UtcNow,
            Bio = ""
        };
         var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            return Ok(new { Message = "User registered successfully" });
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }

// 
// Контроллер для входа в систему!
// 

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var token = _tokenService.CreateToken(user);
            return Ok(new { token });
        }
        return Unauthorized("Invalid email or password");
    }
}
