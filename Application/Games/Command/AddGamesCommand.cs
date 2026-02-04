using MediatR;
using Domain;
using Application.Common.Interfaces;



namespace Application.Games.Commands;

public record AddGameCommand(string Title , string Genre) : IRequest<Guid>;

public class AddGameHandler : IRequestHandler<AddGameCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public AddGameHandler(IApplicationDbContext context)
    {
        _context = context;
    }





    public async Task<Guid> Handle (AddGameCommand requst , CancellationToken cancellationToken)
    {
        
        var game = new Domain.Game
        {
            Title = requst.Title,
            Genre = requst.Genre,
            AddedAt = DateTime.UtcNow
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync(cancellationToken);

        return game.Id;
    }
}
