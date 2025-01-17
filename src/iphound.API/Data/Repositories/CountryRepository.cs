using System.Data;
using Dapper;
using iphound.API.Data.Context;
using iphound.API.Data.Repositories.Interfaces;
using iphound.API.Models.Entities;
using iphound.API.Models.HttpModels.Responses;
using Microsoft.Data.SqlClient;

namespace iphound.API.Data.Repositories;

public class CountryRepository : Repository<Country>, ICountryRepository
{
    private readonly string connString;
    public CountryRepository(AppDbContext context, IConfiguration configuration) : base(context)
    {
        connString = configuration.GetConnectionString("DefaultConnection")!;
    }
    public List<CountryReportResponse> GetCountryReport()
    {
        List<CountryReportResponse> result;
        
        using IDbConnection db = new SqlConnection(connString);
        
        result = db.Query<CountryReportResponse>
            ("SP_SEL_COUNTRY_STATS", commandType: CommandType.StoredProcedure).ToList();

        return result;
    }
}