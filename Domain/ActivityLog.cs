using Microsoft.AspNetCore.Identity;

namespace Domain;

// 
// Время проведенное в приложении!
// 
public class ActivityLog
{
    public Guid Id {get; init; } = Guid.NewGuid();
    public string ActivityName{get; set;} = string.Empty;
    public DateTime StartTime {get; set;} = DateTime.UtcNow;
    public DateTime? EndTime {get; set;}

// 
// Связь с пользователем и игрой!
// 
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid? GameId { get; set; } 
    public Game Game { get; set; } = null!;

    public double DurationMinutes => EndTime.HasValue
    ? (EndTime.Value - StartTime).TotalMinutes
    : (DateTime.UtcNow - StartTime).TotalMinutes;

}