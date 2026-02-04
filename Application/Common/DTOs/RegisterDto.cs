namespace Application.Common.DTOs;

public record RegisterDto(
    string Email, 
    string Password, 
    string DisplayName
    );