using RecruitmentManager.Domain.Primitives;
using RecruitmentManager.Domain.ValueObjects;

namespace RecruitmentManager.Domain.Candidates;

public class Experience : AuditableEntity<Guid>
{
    public Candidate Candidate { get; set; } = default!;

    public Guid CandidateId { get; set; }

    public string Company { get; set; } = default!;

    public string Job { get; set; } = default!;

    public string Description { get; set; } = default!;

    public Period Period { get; set; } = default!;

    public Salary Salary { get; set; } = default!;
}