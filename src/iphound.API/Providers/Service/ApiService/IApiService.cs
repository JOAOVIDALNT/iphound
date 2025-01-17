using iphound.API.Models.HttpModels.Responses;

namespace iphound.API.Providers.Service.ApiService;

public interface IApiService
{
    Task<IpInfoResponse> FetchIpInfo(string ip);
}