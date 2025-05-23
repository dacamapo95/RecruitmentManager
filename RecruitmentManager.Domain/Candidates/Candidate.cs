using RecruitmentManager.Domain.Primitives;
using RecruitmentManager.Domain.ValueObjects;

namespace RecruitmentManager.Domain.Candidates;

public class Candidate : AggregateRoot<Guid>
{
    public FullName FullName { get; set; } = default!;

    public Email Email { get; set; } = default!;

    public DateTime DateOfBirth { get; set; }

    public string PhoneNumber { get; set; }

    public State State { get; set; } = default!;

    public int StateId { get; set; }

    public Address Address { get; set; } = default!;

    public Guid AddressId { get; set; }

    public ICollection<Experience> Experiences { get; set; } = [];



}