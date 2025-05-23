using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentManager.Domain.Candidates;

namespace RecruitmentManager.Infrastructure.Database.Configurations;

public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
{
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.Property(e => e.Company).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Job).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).HasMaxLength(4000);

        builder.OwnsOne(e => e.Period, p =>
        {
            p.Property(pp => pp.BeginDate).HasColumnName("BeginDate").IsRequired();

            p.Property(pp => pp.EndDate).HasColumnName("EndDate").IsRequired(false);
        });

        builder.OwnsOne(e => e.Salary, s =>
        {
            s.Property(sp => sp.Amount)
                .HasColumnName("SalaryAmount")
                .HasColumnType("decimal(8,2)")
                .IsRequired();

            s.Property(sp => sp.Currency)
                .HasColumnName("SalaryCurrency")
                .IsRequired()
                .HasMaxLength(3);
        });

        builder.HasOne(e => e.Candidate)
            .WithMany(c => c.Experiences)
            .HasForeignKey(e => e.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}