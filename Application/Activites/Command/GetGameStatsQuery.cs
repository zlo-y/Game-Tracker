using MediatR;

public record GetGameStatsQuery : IRequest<IEnumerable<GameStatsDto>>;