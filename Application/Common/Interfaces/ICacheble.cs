namespace Application.Common.Interfaces;

public interface ICacheble
{
    string CacheKey{get;}
    TimeSpan? Expiration{get;}
}