using Application.Common.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;

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
        
        modelBuilder.Entity<ActivityLog>().HasKey(e => e.Id);
        
    }

// 
// Сохранение изменений в бд
// 
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
