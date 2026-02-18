using MediatR;
using Application.Common.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Activities.Commands;

// 
// Обработчик для создания новой активности пользователя
// 
public class CreateActivityHandler : IRequestHandler<CreateActivityCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    public CreateActivityHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
{
    var activity = new ActivityLog
    {
        Id = Guid.NewGuid(),
        ActivityName = request.Name,
        StartTime = DateTime.UtcNow,
        GameId = request.GameId,
        UserId = _currentUserService.UserId // Получаем UserId из текущего пользователя
    };

    _context.ActivityLogs.Add(activity);
    
    await _context.SaveChangesAsync(cancellationToken);

    return activity.Id;
}
    
}