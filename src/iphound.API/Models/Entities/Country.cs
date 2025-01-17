using iphound.API.Models.Entities.Base;

namespace iphound.API.Models.Entities;

public class Country : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string TwoLetterCode { get; set; } = string.Empty;
    public string ThreeLetterCode { get; set; } = string.Empty;
    public virtual ICollection<IpAddress> IpAddresses { get; set; } = [];
}