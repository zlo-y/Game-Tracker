using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;


namespace Application.Games.Queries;
// 
// Получение списка игр
// Добавил интерфейс ICacheble для ускоренной работы и получения данных из кеша!
public record GetGamesQuery(string? SearchTerm = null, string? Genre = null) : IRequest<IEnumerable<GameListEntity>>, ICacheble
{
    public string CacheKey => $"games-{SearchTerm ?? "all"}-{Genre ?? "all"}";
    public TimeSpan? Expiration => TimeSpan.FromMinutes(2);
}

public class GetGamesHandler : IRequestHandler<GetGamesQuery , IEnumerable<GameListEntity>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetGamesHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GameListEntity>> Handle (GetGamesQuery request ,CancellationToken cancellationToken)
    {
        var query = _context.Games.AsNoTracking();
        if(!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(g => g.Title.Contains(request.SearchTerm));
        }
        if(!string.IsNullOrWhiteSpace(request.Genre))
        {
            query = query.Where(g => g.Genre == request.Genre);
        }

        return await query
            .ProjectTo<GameListEntity>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}