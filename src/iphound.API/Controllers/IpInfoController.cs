using iphound.API.Models.Entities;
using iphound.API.Models.HttpModels.Responses;
using iphound.API.Providers.Service.ApiService;
using iphound.API.Providers.Service.AppService;
using iphound.API.Providers.Service.DatabaseService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace iphound.API.Controllers;

[ApiController]
[Route("[controller]")]
public class IpInfoController : ControllerBase
{
    [HttpGet("FetchData")]
    public async Task<IActionResult> FetchIpInfo([FromServices] IAppService service, string ip)
    {
        var result = await service.FetchDataAsync(ip);
        return Ok(result);
    }
    
    [HttpGet("CountryReport")]
    public ActionResult<List<CountryReportResponse>> CountryReport([FromServices] IDatabaseService service)
    {
        var result = service.GetCountryReport();
        return Ok(result);
    }
    
}