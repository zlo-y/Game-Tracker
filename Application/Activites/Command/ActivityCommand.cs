using MediatR;
using Domain;
using Application.Common.Interfaces;



namespace Application.Activities.Commands;


// Эта штука — просто почтальон. В ней нет логики, только данные от юзера.
public class ActivityHandler : IRequestHandler<CreateActivityCommand , Guid>
{
    private readonly IApplicationDbContext _context;
    public ActivityHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {

// чтобы у юзеров из разных городов время совпадало!
        var activity = new ActivityLog
        {
            ActivityName = request.Name,
            StartTime = DateTime.UtcNow,
            UserId = request.UserId, 
            GameId = request.GameId
        };
// Закидываем в очередь на добавление
        _context.ActivityLogs.Add(activity);
// Физически пишем в БД. Если БД отвалится — упадем тут.
        await _context.SaveChangesAsync(cancellationToken);

// Возвращаем ID, чтобы фронтенд мог потом "закрыть" эту же активность по её ID
        return activity.Id;
    }
} 
