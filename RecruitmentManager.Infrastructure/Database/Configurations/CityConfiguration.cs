using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentManager.Domain.Countries;

namespace RecruitmentManager.Infrastructure.Database.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.Property(c => c.Name).IsRequired()
            .HasMaxLength(100);
    }
}