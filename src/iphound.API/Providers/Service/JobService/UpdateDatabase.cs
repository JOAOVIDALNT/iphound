using iphound.API.Data.Repositories;
using iphound.API.Data.Repositories.Interfaces;
using iphound.API.Models.Entities;
using iphound.API.Providers.Service.ApiService;
using iphound.API.Providers.Service.CacheService;
using iphound.API.Providers.Service.DatabaseService;
using iphound.API.Utils;

namespace iphound.API.Providers.Service.JobService;

public class UpdateDatabase
{
    private readonly IIpAddressRepository _ipAddressRepository;
    private readonly IDatabaseService _databaseService;
    private readonly IApiService _apiService;
    private readonly ICacheService _cacheService;
    private readonly ILogger<UpdateDatabase> _logger;
    
    public UpdateDatabase(IIpAddressRepository ipAddressRepository, IApiService apiService, ICacheService cacheService,
        ILogger<UpdateDatabase> logger, IDatabaseService databaseService)
    {
        _ipAddressRepository = ipAddressRepository;
        _apiService = apiService;
        _cacheService = cacheService;
        _logger = logger;
        _databaseService = databaseService;
    }
    
    public async Task ExecuteAsync()
    {
        const int batchSize = 100;
        int page = 1;

        _logger.LogInformation("Starting database update job...");

        while (true)
        {
            List<IpAddress>? ips = await _ipAddressRepository.GetIpAddressListWithCountryInfo(batchSize, page);

            if (ips == null || !ips.Any())
                break;

            foreach (var ip in ips)
            {
                try
                {
                    var latestInfo = await _apiService.FetchIpInfo(ip.Ip);
                    
                    if (!latestInfo.CompareInfo(ip)) // TODO: EXTENSION METHOD TO COMPARE
                    {
                        var entity = latestInfo.MergeInfo(ip);
                        entity.UpdatedAt = DateTime.UtcNow;
                        await _databaseService.UpdateIpInfoAsync(entity);

                        // Invalidate the cache
                        await _cacheService.RemoveAsync($"{ip.Ip}");

                        _logger.LogInformation($"Updated IP: {ip.Ip}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error updating IP: {ip.Ip}");
                }
            }

            page++;
        }

        _logger.LogInformation("IP update job completed.");
    }
    
}