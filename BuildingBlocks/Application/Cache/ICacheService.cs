namespace Booket.BuildingBlocks.Application.Cache;

public interface ICacheService
{
    Task SetAsync<T>(string key, T value, TimeSpan expiration, CancellationToken cancellationToken = default);
    Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default);
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
}