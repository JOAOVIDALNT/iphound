using iphound.API.Models.Entities;
using iphound.API.Models.HttpModels.Responses;
using iphound.API.Providers.Service.Ip2cService;
using iphound.API.Providers.Service.IpManagmentService;
using iphound.API.Providers.Service.DatabaseService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace iphound.API.Controllers;

[ApiController]
[Route("[controller]")]
public class IpInfoController : ControllerBase
{
    [HttpGet("FetchData")]
    [ProducesResponseType(typeof(IpInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> FetchIpInfo([FromServices] IIpManagmentService service, string ip)
    {
        var result = await service.FetchDataAsync(ip);
        return Ok(result);
    }
    
    [HttpGet("CountryReport")]
    [ProducesResponseType(typeof(IpInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult<List<CountryReportResponse>> CountryReport([FromServices] IDatabaseService service)
    {
        var result = service.GetCountryReport();
        return Ok(result);
    }
    
}