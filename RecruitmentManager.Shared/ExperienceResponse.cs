namespace RecruitmentManager.Shared;

public sealed record ExperienceResponse(
    Guid Id,
    string Company,
    string Job,
    string Description,
    DateTime StartDate,
    DateTime? EndDate,
    decimal Salary,
    string Currency
);