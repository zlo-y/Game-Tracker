using MediatR;
using Application.Common.Interfaces;
using Domain;
using Application.Activities.Commands;



namespace Application.Activities.Commands;



public class CreateActivityHandler : IRequestHandler<CreateActivityCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    public CreateActivityHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
{
    var activity = new ActivityLog
    {
        Id = Guid.NewGuid(),
        ActivityName = request.Name,
        StartTime = DateTime.UtcNow,
        GameId = request.GameId,
        UserId = request.UserId
    };

    _context.ActivityLogs.Add(activity);
    
    await _context.SaveChangesAsync(cancellationToken);

    return activity.Id;
}
    
}