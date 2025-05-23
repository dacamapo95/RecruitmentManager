using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentManager.Domain.Candidates;

namespace RecruitmentManager.Infrastructure.Database.Configurations;

public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.OwnsOne(c => c.FullName, n =>
        {
            n.Property(p => p.FirstName)
                .HasColumnName("FirstName").IsRequired()
                .HasMaxLength(50);
            n.Property(p => p.SurName).HasColumnName("SurName").IsRequired().HasMaxLength(150);
            n.HasIndex(p => p.FirstName);
            n.HasIndex(p => p.SurName);
            n.HasIndex(p => new { p.FirstName, p.SurName });
        });

        builder.OwnsOne(c => c.Email, e =>
        {
            e.Property(p => p.Value).HasColumnName("Email").IsRequired().HasMaxLength(250);
            e.HasIndex(p => p.Value).IsUnique();
        });

        builder.Property(c => c.DateOfBirth).IsRequired();

        builder.Property(c => c.PhoneNumber).HasMaxLength(15);

        builder.HasOne(c => c.State)
            .WithMany(s => s.Candidates)
            .HasForeignKey(c => c.StateId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Address)
            .WithOne()
            .HasForeignKey<Candidate>(x => x.AddressId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Experiences)
            .WithOne(e => e.Candidate)
            .HasForeignKey(e => e.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}