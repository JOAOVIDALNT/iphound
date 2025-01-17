using iphound.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iphound.API.Data.Context.Mapping;

public class CountryMap : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries")
            .HasKey(x => x.Id);
        
        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        builder.Property(x => x.TwoLetterCode).HasMaxLength(2).IsRequired();
        builder.Property(x => x.ThreeLetterCode).HasMaxLength(3).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}