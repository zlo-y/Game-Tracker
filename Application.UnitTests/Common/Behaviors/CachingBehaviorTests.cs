using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Application.Common.Interfaces;
using Application.Common.Behaviors;



namespace Application.UnitTests.Common.Behaviors;

public class CachingBehaviorTests
{
     private readonly Mock<IDistributedCache> _mockCache;
     private readonly Mock<RequestHandlerDelegate<TestResponse>> _mockNext;

     public CachingBehaviorTests()
     {
         _mockCache = new Mock<IDistributedCache>();
         _mockNext = new Mock<RequestHandlerDelegate<TestResponse>>();
     }  

    [Fact]
    public async Task Handle_ShouldReturnCachedResponse_WhenDataExists()
    {
        var request = new TestRequest("test-key");
        var cachedResponse = new TestResponse("Cached Message");
        var serializedResponse = System.Text.Json.JsonSerializer.Serialize(cachedResponse);
        var encodedData = System.Text.Encoding.UTF8.GetBytes(serializedResponse);

        _mockCache.Setup(x => x.GetAsync(request.CacheKey, It.IsAny<CancellationToken>()))
            .ReturnsAsync(encodedData);

        var behavior = new CachingBehavior<TestRequest, TestResponse>(_mockCache.Object);
    }


}

public record TestRequest(string CacheKey) : IRequest<TestResponse>, ICacheble 
{
    public TimeSpan? Expiration => TimeSpan.FromMinutes(1);
}

public record TestResponse(string Message);