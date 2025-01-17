using iphound.API.Models.Entities;
using iphound.API.Models.HttpModels.Responses;

namespace iphound.API.Providers.Service.DatabaseService;

public interface IDatabaseService
{
    Task<IpInfoResponse?> GetIpInfoAsync(string ipAddress);
    Task SaveIpInfoAsync(IpInfoResponse ipInfoResponse);
    Task UpdateIpInfoAsync(IpAddress ipAddress);
    List<CountryReportResponse> GetCountryReport();
}