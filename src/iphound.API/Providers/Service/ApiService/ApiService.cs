using iphound.API.Models.HttpModels.Responses;
using iphound.API.Utils;

namespace iphound.API.Providers.Service.ApiService;

public class ApiService(HttpClient httpClient) : IApiService
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