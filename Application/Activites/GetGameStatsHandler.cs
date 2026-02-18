using MediatR;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Activites.Command;


// 
// Обработчик для получения статистики по играм пользователя 
// 
public class GetGamesStatsHandler : IRequestHandler<GetGameStatsQuery, IEnumerable<GameStatsDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetGamesStatsHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<GameStatsDto>> Handle(GetGameStatsQuery request, CancellationToken cancellationToken)
    {
      var userId = _currentUserService.UserId;

      var stats = await _context.ActivityLogs
           .AsNoTracking()
           .Where(a => a.UserId == userId && a.GameId != null && a.EndTime != null)
           .GroupBy(a => a.Game.Title)
           .Select(group => new GameStatsDto
           {
               GameTitle = group.Key,
               SessionsCount = group.Count(),
         TotalPlayTime = Math.Round(group.Sum(a => 
                (a.EndTime!.Value - a.StartTime).TotalMinutes / 60.0), 1)
        })
         .OrderByDescending(s => s.TotalPlayTime)
        .ToListAsync(cancellationToken);

        return stats;
    
}
}