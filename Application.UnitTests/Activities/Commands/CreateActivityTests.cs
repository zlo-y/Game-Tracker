using Application.Activities.Commands;
using Application.Common.Interfaces;
using Moq;
using Moq.EntityFrameworkCore;
using Application.Activities.Handlers;


namespace Application.UnitTests.Activities.Commands;

// 
// Тест для CreateActivityHandler. Проверяем, что при создании новой активности предыдущая активность юзера закрывается (устанавливается EndTime), а новая активность добавляется в БД.
// 
public class CreateActivityTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Guid _currentUserId = Guid.NewGuid();

    public CreateActivityTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
        _currentUserServiceMock.Setup(s => s.UserId).Returns(_currentUserId);
    }

    [Fact]
    public async Task Handle_ShouldCreateNewActivity_AndClosePreviousOne()
    {
        var existingActivity = new Domain.ActivityLog
        {
            Id = Guid.NewGuid(),
            UserId = _currentUserId,
            StartTime = DateTime.UtcNow.AddHours(-1),
            EndTime = null
        };

        _contextMock.Setup(x => x.ActivityLogs)
            .ReturnsDbSet(new List<Domain.ActivityLog> { existingActivity });

        var handler = new CreateActivityHandler(_contextMock.Object, _currentUserServiceMock.Object);
        var command = new CreateActivityCommand(
            Name: "Playing",
            GameId: Guid.NewGuid(),
            UserId: _currentUserId
        );

        var resultId = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(existingActivity.EndTime);

// 
// Проверяем, что новая активность добавлена
// 
        _contextMock.Verify(x => x.ActivityLogs.Add(It.IsAny<Domain.ActivityLog>()), Times.Once);

// 
// Проверяем, что изменения сохранены
// 
        _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        Assert.NotEqual(Guid.Empty, resultId);
  
    }
}