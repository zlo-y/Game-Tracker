
using Application.Activities.Commands;
using Application.Activities.Handlers;
using Application.Common.Interfaces;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;    

namespace Application.UnitTests.Activities.Commands;

public class StopActivityTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;

    public StopActivityTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenActivityBelongsToAnotherUser()
    {
        var userId = Guid.NewGuid();
        var anotherUserId = Guid.NewGuid();
        var activityId = Guid.NewGuid();

        _currentUserServiceMock 
             .Setup(s => s.UserId)
             .Returns(userId);

        var activity = new Domain.ActivityLog
        {
            Id = activityId,
            UserId = anotherUserId
        };

        _contextMock.Setup(x => x.ActivityLogs)
            .ReturnsDbSet(new List<Domain.ActivityLog> { activity });

        var handler = new StopActivityHandler(_contextMock.Object, _currentUserServiceMock.Object);
        var command = new StopActivityCommand(activityId, userId);

        var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));

        Assert.Equal("Нельзя завершить чужую активность", exception.Message);
    }
}