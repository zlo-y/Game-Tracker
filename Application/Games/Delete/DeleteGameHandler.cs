using Domain;
using MediatR;
using Application.Common.Interfaces;
using Application.Games.Delete;

namespace Application.Games.Handler;

public class DeleteGameHandler : IRequestHandler<DeleteGameCommand , Guid>
{
    private readonly IApplicationDbContext _context;

    public DeleteGameHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle (DeleteGameCommand request , CancellationToken cancellationToken)
    {
        var game = await _context.Games.FindAsync(new object[] {request.Id}, cancellationToken);
        if(game == null)throw new Exception("Приложение(игра) не найдено");

        _context.Games.Remove(game);
        await _context.SaveChangesAsync(cancellationToken);

        return game.Id;
    }
}