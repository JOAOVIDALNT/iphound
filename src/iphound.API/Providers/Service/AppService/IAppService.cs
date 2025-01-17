using iphound.API.Models.HttpModels.Responses;

namespace iphound.API.Providers.Service.AppService;

public interface IAppService
{
    Task<IpInfoResponse> FetchDataAsync(string ipAddress);
}