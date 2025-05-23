using Microsoft.EntityFrameworkCore;
using RecruitmentManager.Domain.Candidates;

namespace RecruitmentManager.Infrastructure.Database.Repositories;
internal sealed class CandidateRepository(ApplicationDbContext context) 
    : Repository<Candidate, Guid>(context), ICandidateRepository
{
    public async Task<Candidate?> GetWithExperiences(Guid candidateId, CancellationToken cancellationToken = default)
    {
        return await Query
            .Include(c => c.FullName)
            .Include(c => c.Email)
            .Include(c => c.Address)
            .Include(c => c.Experiences)
            .Include(c => c.Address)
            .ThenInclude(a => a.City)
            .FirstOrDefaultAsync(c => c.Id == candidateId);
    }
}
