using RecruitmentManager.Domain.Interfaces;
using System.Security.Cryptography;

namespace RecruitmentManager.Domain.Candidates;

public interface ICandidateRepository : IRepository<Candidate, Guid> 
{
    Task<Candidate?> GetWithExperiences(Guid candidateId, CancellationToken cancellationToken = default);
}