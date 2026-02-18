using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class User : IdentityUser<Guid>
    {
// 
// Отображаемое имя пользователя
// 
        public string? DisplayName { get; set;}
// 
// Ссылка на аватарку пользователя
// 
     public string? AvatarUrl { get; set; }
// 
// информация о пользователе
// 
        public DateTime CreatedAt { get; set;  } = DateTime.UtcNow;
        public string Bio { get; set; } = string.Empty;

// 
// Игры пользователя
// 
    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    }
}