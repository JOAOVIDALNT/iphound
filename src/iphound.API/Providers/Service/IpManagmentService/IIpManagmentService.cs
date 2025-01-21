using iphound.API.Models.HttpModels.Responses;

namespace iphound.API.Providers.Service.IpManagmentService;

public interface IIpManagmentService
{
    Task<IpInfoResponse> FetchDataAsync(string ipAddress);
}