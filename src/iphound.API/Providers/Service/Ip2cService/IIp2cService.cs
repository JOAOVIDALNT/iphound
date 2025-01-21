using iphound.API.Models.HttpModels.Responses;

namespace iphound.API.Providers.Service.Ip2cService;

public interface IIp2cService
{
    Task<IpInfoResponse> FetchIpInfo(string ip);
}