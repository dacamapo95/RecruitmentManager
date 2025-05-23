using Microsoft.EntityFrameworkCore;
using RecruitmentManager.Domain.Candidates;
using RecruitmentManager.Domain.Countries;

namespace RecruitmentManager.Application.Core.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Candidate> Candidates { get; }
    DbSet<Address> Addresses { get; }
    DbSet<State> States { get; }
    DbSet<Experience> Experiences { get; }
    DbSet<City> Cities { get; }
    DbSet<Country> Countries { get; }
}