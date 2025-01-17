namespace iphound.API.Models.HttpModels.Responses;

public class CountryReportResponse
{
    public string CountryName { get; set; } = string.Empty;
    public int AddressesCount { get; set; }
    public DateTime LastAddressUpdated { get; set; }
}