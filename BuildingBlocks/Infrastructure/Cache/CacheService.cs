using System.Text.Json;
using Booket.BuildingBlocks.Application.Cache;
using Microsoft.Extensions.Caching.Distributed;

namespace Booket.BuildingBlocks.Infrastructure.Cache;

public class CacheService(IDistributedCache distributedCache) : ICacheService
{
    public async Task SetAsync<T>(string key, T value, TimeSpan expiration, CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };

        var jsonData = JsonSerializer.Serialize(value);
        await distributedCache.SetStringAsync(key, jsonData, options, cancellationToken);
    }

    public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var jsonData = await distributedCache.GetStringAsync(key, cancellationToken);
        return jsonData is null ? default : JsonSerializer.Deserialize<T>(jsonData);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await distributedCache.RemoveAsync(key, cancellationToken);
    }
}