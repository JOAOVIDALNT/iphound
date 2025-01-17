using iphound.API.Models.HttpModels.Responses;

namespace iphound.API.Providers.Service.CacheService;

public interface ICacheService
{
    Task<string?> GetAsync(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);
    Task RemoveAsync(string key);
}