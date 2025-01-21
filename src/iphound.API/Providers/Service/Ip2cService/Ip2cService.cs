using iphound.API.Models.HttpModels.Responses;
using iphound.API.Utils;

namespace iphound.API.Providers.Service.Ip2cService;

public class Ip2cService(HttpClient httpClient) : IIp2cService
{
    public async Task<IpInfoResponse> FetchIpInfo(string ip)
    {
        var request = await httpClient.GetAsync(ip);

        if (request.IsSuccessStatusCode)
        {
            var result = request.Content.ReadAsStringAsync().Result;
            return result.ApiToIpInfo(ip);
        }
        
        return new IpInfoResponse() { Success = false };
    }
}