using iphound.API.Models.HttpModels.Responses;
using iphound.API.Providers.Service.Ip2cService;
using iphound.API.Providers.Service.CacheService;
using iphound.API.Providers.Service.DatabaseService;
using iphound.API.Providers.Validator;
using Newtonsoft.Json;

namespace iphound.API.Providers.Service.IpManagmentService;

public class IpManagmentService : IIpManagmentService
{
    private readonly ICacheService _cacheService;
    private readonly IDatabaseService _databaseService;
    private readonly IIp2cService _apiService;
    
    public IpManagmentService(ICacheService cacheService, IDatabaseService databaseService, IIp2cService apiService)
    {
        _cacheService = cacheService;
        _databaseService = databaseService;
        _apiService = apiService;
    }
    public async Task<IpInfoResponse> FetchDataAsync(string ipAddress)
    {
        ipAddress.ValidateIp();

        var localData = await GetLocalDataAsync(ipAddress);

        if (localData != null)
            return localData;

        var apiData = await _apiService.FetchIpInfo(ipAddress);
        
        await SaveDataAsync(apiData);
        
        return apiData;
    }

    private async Task<bool> SaveDataAsync(IpInfoResponse ipInfoResponse)
    {
        await _cacheService.SetAsync(ipInfoResponse.IpAddress, ipInfoResponse);
        await _databaseService.SaveIpInfoAsync(ipInfoResponse);
        return true;
    }

    private async Task<IpInfoResponse?> GetLocalDataAsync(string ipAddress)
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
        return null;
    }
} 