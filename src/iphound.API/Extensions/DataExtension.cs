using iphound.API.Data.Context;
using iphound.API.Data.Repositories;
using iphound.API.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace iphound.API.Extensions;

public static class DataExtension
{
    public static void AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlServer(configuration);
        services.AddRedis();
        services.AddRepositories();
    }

    private static void AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    }

    private static void AddRedis(this IServiceCollection services)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "localhost:6379";
        });
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IIpAddressRepository, IpAddressRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
    }
}