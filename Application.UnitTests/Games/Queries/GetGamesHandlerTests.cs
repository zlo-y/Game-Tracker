using Application.Common.Interfaces;
using AutoMapper;
using Moq;
using Application.Common.Mappings;
using Xunit;
using Moq.EntityFrameworkCore;
using Application.Games.Queries;
using FluentAssertions;


namespace Application.UnitTests.Games.Queries;


// 
// Эти тесты предназначены для проверки правильности работы обработчика запросов на получение списка игр.
// 
public class GetGamesHandlerTests
{
    private readonly Mock<IApplicationDbContext> _mockContext;
    private readonly IMapper _mapper;

    public GetGamesHandlerTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();

        var loggerFactory = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();

    var configuration = new MapperConfiguration(cfg => 
    {
        cfg.AddProfile<MappingProfile>(); 
    }, loggerFactory); 

    _mapper = configuration.CreateMapper();
    }

// 
// Тест проверяет, что при вызове обработчика запросов на получение списка игр, он возвращает правильный список игр из базы данных.
// 
    [Fact]
    public async Task Handle_ShouldReturnGamesList()
    {
        var games = new List<Domain.Game>
        {
          new() { Id = Guid.NewGuid(), Title = "The Witcher 3", Genre = "RPG" },
          new() { Id = Guid.NewGuid(), Title = "FIFA 23", Genre = "Sports" }
        };

        _mockContext.Setup(x => x.Games).ReturnsDbSet(games);

        var handler = new GetGamesHandler(_mockContext.Object, _mapper);
        var query = new GetGamesQuery(null , null);

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.First().Title.Should().Be("The Witcher 3");
    }


  [Fact]
  public async Task Handle_WithSearchTerm_ShouldFilterGames()
    {
        var games = new List<Domain.Game>
        {
            new() { Id = Guid.NewGuid(), Title = "Cyberpunk 2077" },
            new() { Id = Guid.NewGuid(), Title = "Minecraft" }
        };

        _mockContext.Setup(x => x.Games).ReturnsDbSet(games);

        var handler = new GetGamesHandler(_mockContext.Object, _mapper);
        var query = new GetGamesQuery("Cyberpunk", null);

        var result = await handler.Handle(query, CancellationToken.None);

        
        result.Should().HaveCount(1);
        result.First().Title.Should().Be("Cyberpunk 2077");
        }
}
