using iphound.API.Models.Entities;
using iphound.API.Models.HttpModels.Responses;

namespace iphound.API.Data.Repositories.Interfaces;

public interface IIpAddressRepository : IRepository<IpAddress>
{
    Task<IpAddress?> GetIpAddressWithCountryInfo(string ipAddress);
    Task<List<IpAddress>?> GetIpAddressListWithCountryInfo(int pageSize = 0, int pageNumber = 1);
}