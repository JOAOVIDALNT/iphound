using iphound.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iphound.API.Data.Context.Mapping;

public class IpAddressMap : IEntityTypeConfiguration<IpAddress>
{
    public void Configure(EntityTypeBuilder<IpAddress> builder)
    {
        builder.ToTable("IpAddresses")
            .HasKey(x => x.Id);
        
        builder.Property(x => x.Ip).IsRequired().HasColumnName("IP").HasMaxLength(15);
        builder.Property(x => x.CountryId).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();

        builder.HasOne(a => a.Country)
            .WithMany(c => c.IpAddresses)
            .HasForeignKey(a => a.CountryId);
    }
}