using iphound.API.Models.Entities;
using iphound.API.Models.HttpModels.Responses;

namespace iphound.API.Data.Repositories.Interfaces;

public interface ICountryRepository : IRepository<Country>
{
    List<CountryReportResponse> GetCountryReport();
}