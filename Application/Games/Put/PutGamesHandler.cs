using Domain;
using Application.Common.Interfaces;
using MediatR;
using Application.Games.Put;


namespace Application.Games.Handler;


public class PutGamesHandler : IRequestHandler<PutGameCommand , Guid>
{
    private readonly IApplicationDbContext _context;

    public PutGamesHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle (PutGameCommand request , CancellationToken cancellationToken)
    {
        var game = await _context.Games.FindAsync(new object[] {request.Id} , cancellationToken);
        if(game == null) throw new Exception("Игра не найдена, обновлять нечего!");

        game.Title = request.Title;
        await _context.SaveChangesAsync(cancellationToken);
        return game.Id;
    }
}