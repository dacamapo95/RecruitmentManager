using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentManager.Domain.Countries;

namespace RecruitmentManager.Infrastructure.Database.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.Property(c => c.Name).IsRequired()
            .HasMaxLength(50);

        builder.HasMany(c => c.Cities)
            .WithOne(cy => cy.Country)
            .HasForeignKey(cy => cy.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}