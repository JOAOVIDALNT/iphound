using iphound.API.Models.HttpModels.Responses;
using iphound.API.Providers.Service.ApiService;
using iphound.API.Providers.Service.CacheService;
using iphound.API.Providers.Service.DatabaseService;
using Newtonsoft.Json;

namespace iphound.API.Providers.Service.AppService;

public class AppService : IAppService
{
    private readonly ICacheService _cacheService;
    private readonly IDatabaseService _databaseService;
    private readonly IApiService _apiService;
    
    public AppService(ICacheService cacheService, IDatabaseService databaseService, IApiService apiService)
    {
        _cacheService = cacheService;
        _databaseService = databaseService;
        _apiService = apiService;
    }
    public async Task<IpInfoResponse> FetchDataAsync(string ipAddress)
    {
        var key = $"{ipAddress}";

        var cachedData = await _cacheService.GetAsync(key);
        
        if (cachedData != null)
            return JsonConvert.DeserializeObject<IpInfoResponse>(cachedData);
        
        var persistedData = await _databaseService.GetIpInfoAsync(ipAddress);

        if (persistedData != null)
        {
            await _cacheService.SetAsync(key, persistedData);
            return persistedData;
        }
        
        var apiData = await _apiService.FetchIpInfo(ipAddress);
        
        var test = await SaveDataAsync(apiData);
        
        return apiData;
    }

    private async Task<bool> SaveDataAsync(IpInfoResponse ipInfoResponse)
    {
        await _cacheService.SetAsync(ipInfoResponse.IpAddress, ipInfoResponse);
        await _databaseService.SaveIpInfoAsync(ipInfoResponse);
        return true;
    }
} 