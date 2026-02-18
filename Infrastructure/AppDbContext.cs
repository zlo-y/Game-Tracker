using Application.Common.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace Infrastructure;

// 
// Класс для работы с бд и дальнейшей миграции!
// 
public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid> , IApplicationDbContext
{
    public AppDbContext (DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<Game> Games {get; set;}
    public DbSet<ActivityLog> ActivityLogs{get; set;}

// 
// Настройка модели данных и связей между сущностями
// 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
     modelBuilder.Entity<Game>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Title).IsRequired();
        entity.Property(e => e.Genre).IsRequired();

        entity.HasOne(e => e.User)
            .WithMany(u => u.Games)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    });

    // Конфигурация Логов 
modelBuilder.Entity<ActivityLog>(entity =>
{
    entity.HasKey(e => e.Id);

    entity.HasOne(e => e.User)
        .WithMany(u => u.ActivityLogs) 
        .HasForeignKey(e => e.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    entity.HasOne(e => e.Game)
        .WithMany()
        .HasForeignKey(e => e.GameId)
        .OnDelete(DeleteBehavior.SetNull);
});
        
    }

// 
// Сохранение изменений в бд
// 
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
