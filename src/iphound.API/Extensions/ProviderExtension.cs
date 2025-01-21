using iphound.API.Providers.Service.Ip2cService;
using iphound.API.Providers.Service.IpManagmentService;
using iphound.API.Providers.Service.CacheService;
using iphound.API.Providers.Service.DatabaseService;
using iphound.API.Providers.Service.JobService;

namespace iphound.API.Extensions;

public static class ProviderExtension
{
    public static void AddProviders(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddHttpClient();
        services.AddBackgroundService();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IIp2cService, Ip2cService>();
        services.AddScoped<IIpManagmentService, IIpManagmentService>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IDatabaseService, DatabaseService>();
        services.AddScoped<UpdateDatabase>();
    }

    private static void AddHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient<IIp2cService, Ip2cService>(
            client =>
            {
                client.BaseAddress = new Uri("https://ip2c.org");
            });
    }
    
    private static void AddBackgroundService(this IServiceCollection services)
    {
        services.AddHostedService<UpdateDatabaseBackgroundService>();
    }
}