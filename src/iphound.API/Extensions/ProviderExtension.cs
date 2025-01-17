using iphound.API.Providers.Service.ApiService;
using iphound.API.Providers.Service.AppService;
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
        services.AddScoped<IApiService, ApiService>();
        services.AddScoped<IAppService, AppService>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IDatabaseService, DatabaseService>();
        services.AddScoped<UpdateDatabase>();
    }

    private static void AddHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient<IApiService, ApiService>(
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