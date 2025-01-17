namespace iphound.API.Models.HttpModels.Responses;

public class IpInfoResponse
{
    public string IpAddress { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public string TwoLetterCode { get; set; } = string.Empty;
    public string ThreeLetterCode { get; set; } = string.Empty;
    public bool Success { get; set; }
}