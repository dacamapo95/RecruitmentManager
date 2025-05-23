using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentManager.Domain.Candidates;

namespace RecruitmentManager.Infrastructure.Database.Configurations;

public class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.Property(s => s.Name).IsRequired()
            .HasMaxLength(20);
    }
}