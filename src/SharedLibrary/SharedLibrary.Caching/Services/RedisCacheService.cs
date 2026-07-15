using System.Text.Json;
using SharedLibrary.Caching.Interfaces;
using StackExchange.Redis;

namespace SharedLibrary.Caching.Services;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<T?> GetDataAsync<T>(string key)
    {
        var value = await _database.StringGetAsync(key);

        if (!value.HasValue)
            return default;

        return JsonSerializer.Deserialize<T>(value!);
    }

    public async Task SetDataAsync<T>(
    string key,
    T value,
    TimeSpan? expiry = null)
    {
        var json = JsonSerializer.Serialize(value);

        await _database.StringSetAsync(
            key,
            json,
            expiry ?? TimeSpan.FromMinutes(30));
    }

    public async Task RemoveDataAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }
}