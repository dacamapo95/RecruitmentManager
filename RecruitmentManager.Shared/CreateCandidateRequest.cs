namespace RecruitmentManager.Shared;

public sealed record CreateCandidateRequest(
    string FirstName,
    string SurName,
    DateTime DateOfBirth,
    string Email,
    string? PhoneNumber,
    Guid CityId,
    string? ZipCode,
    string? Street,
    int? StateId,
    List<CreateExperienceRequest> Experiences);


public sealed record CreateExperienceRequest(
    string Company,
    string Job,
    string Description,
    DateTime StartDate,
    DateTime? EndDate,
    decimal Salary,
    string Currency
);

