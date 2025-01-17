using iphound.API.Models.Entities;
using iphound.API.Models.HttpModels.Responses;

namespace iphound.API.Utils;

public static class ResponseMapping
{
    public static IpInfoResponse ApiToIpInfo(this string content, string ipAddress)
    {
        var parts = content.Split(';');
        
        var response = new IpInfoResponse
        {
            IpAddress = ipAddress,
            TwoLetterCode = parts[1],
            ThreeLetterCode = parts[2],
            CountryName = parts[3],
            Success = true
        };
        return response;
    }

    public static IpInfoResponse MapFromEntity(this IpAddress ipAddress)
    {
        return new IpInfoResponse
        {
            IpAddress = ipAddress.Ip,
            CountryName = ipAddress.Country!.Name,
            TwoLetterCode = ipAddress.Country!.TwoLetterCode,
            ThreeLetterCode = ipAddress.Country!.ThreeLetterCode,
            Success = true
        };
    }

    public static IpAddress MapToEntity(this IpInfoResponse ipInfoResponse)
    {
        return new IpAddress
        {
            Ip = ipInfoResponse.IpAddress,
            Country = new Country
            {
                Name = ipInfoResponse.CountryName,
                TwoLetterCode = ipInfoResponse.TwoLetterCode,
                ThreeLetterCode = ipInfoResponse.ThreeLetterCode
            }
        };
    }

    public static bool CompareInfo(this IpInfoResponse ipInfoResponse, IpAddress ipAddress)
    {
        return 
            ipInfoResponse.CountryName == ipAddress.Country!.Name 
            && ipInfoResponse.TwoLetterCode == ipAddress.Country!.TwoLetterCode 
            && ipInfoResponse.ThreeLetterCode == ipAddress.Country!.ThreeLetterCode;
    }
    
    public static IpAddress MergeInfo(this IpInfoResponse ipInfoResponse, IpAddress ipAddress)
    {
        ipAddress.Country!.Name = ipInfoResponse.CountryName;
        ipAddress.Country!.TwoLetterCode = ipInfoResponse.TwoLetterCode;
        ipAddress.Country!.ThreeLetterCode = ipInfoResponse.ThreeLetterCode;
        return ipAddress;
    }
}