using RecruitmentManager.Domain.Primitives;

namespace RecruitmentManager.Domain.Candidates;

public sealed class State : MasterEntity<int>
{
    public ICollection<Candidate> Candidates { get; set; } = [];
}
