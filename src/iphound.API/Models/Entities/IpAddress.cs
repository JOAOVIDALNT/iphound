using iphound.API.Models.Entities.Base;

namespace iphound.API.Models.Entities;

public class IpAddress : BaseEntity
{
    public string Ip { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public int CountryId { get; set; }
    public virtual Country? Country { get; set; }
    
}