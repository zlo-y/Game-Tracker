using MediatR;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Activites.Command;

// 
// Этот обработчик отвечает за "остановку" активности, то есть за установку EndTime для уже существующей активности.
// 
public class StopActivityHandler : IRequestHandler<StopActivityCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public StopActivityHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

// 
// Проверяем, что активность существует, принадлежит юзеру и не завершена. Если все ок — ставим EndTime и сохраняем.
// 

    public async Task<Unit> Handle (StopActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await _context.ActivityLogs
        .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if(activity == null)
        {
            throw new Exception("Активность не найдена");
        }

        if(activity.UserId != _currentUserService.UserId)
        {
            throw new Exception("Нельзя завершить чужую активность");
        }

        if(activity.EndTime != null)
        {
            throw new Exception("Активность уже завершена");
        }

        activity.EndTime = DateTime.UtcNow;

        var duration = activity.EndTime.Value - activity.StartTime;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    
    }
}