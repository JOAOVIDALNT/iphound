using iphound.API.Data.Repositories.Interfaces;
using iphound.API.Models.Entities;
using iphound.API.Models.HttpModels.Responses;
using iphound.API.Utils;

namespace iphound.API.Providers.Service.DatabaseService;

public class DatabaseService : IDatabaseService
{
    private readonly IIpAddressRepository _ipAddressRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DatabaseService(IIpAddressRepository ipAddressRepository, ICountryRepository countryRepository, IUnitOfWork unitOfWork)
    {
        _ipAddressRepository = ipAddressRepository;
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IpInfoResponse?> GetIpInfoAsync(string ipAddress)
    {
        var entity = await _ipAddressRepository.GetIpAddressWithCountryInfo(ipAddress);

        return entity?.MapFromEntity();
    }

    public async Task SaveIpInfoAsync(IpInfoResponse ipInfoResponse)
    {
        await _ipAddressRepository.CreateAsync(ipInfoResponse.MapToEntity());
        await _unitOfWork.Commit();
    }
    
    public async Task UpdateIpInfoAsync(IpAddress ipAddress)
    {
        _ipAddressRepository.Update(ipAddress);
        await _unitOfWork.Commit();
    }

    public List<CountryReportResponse> GetCountryReport()
    {
        return _countryRepository.GetCountryReport();
    }
}