namespace RecruitmentManager.Shared;
public sealed record CandidateItemResponse(
    Guid Id,
    string FirstName,
    string SurName,
    string Email,
    int StateId,
    DateTime CreatedAtUtc
    );
