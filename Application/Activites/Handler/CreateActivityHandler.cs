using MediatR;
using Application.Common.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Application.Activities.Commands;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;


namespace Application.Activities.Handlers;

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
    var userId = _currentUserService.UserId;
    var activeSession = await _context.ActivityLogs
        .FirstOrDefaultAsync(a => a.UserId == userId && a.EndTime == null, cancellationToken);
    
    if(activeSession != null)
        {
            activeSession.EndTime = DateTime.UtcNow;
        }
        
    var activity = new ActivityLog
    {
        Id = Guid.NewGuid(),
        ActivityName = request.Name,
        StartTime = DateTime.UtcNow,
        GameId = request.GameId,
        UserId = _currentUserService.UserId 
    };

    _context.ActivityLogs.Add(activity);
    
    await _context.SaveChangesAsync(cancellationToken);

    return activity.Id;
}
    
}