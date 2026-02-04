using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;


public interface IApplicationDbContext
{
    DbSet<ActivityLog> ActivityLogs{get;}
    DbSet<Domain.Game> Games {get;}

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}