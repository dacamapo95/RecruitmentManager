namespace RecruitmentManager.Shared;
public sealed record EditCandidateRequest(
    string FirstName,
    string SurName,
    DateTime DateOfBirth,
    string Email,
    string? PhoneNumber,
    Guid CityId,
    string? ZipCode,
    string? Street,
    int StateId,
    List<EditExperienceRequest> Experiences);

public sealed record EditExperienceRequest(
    Guid? Id,
    string Company,
    string Job,
    string Description,
    DateTime StartDate,
    DateTime? EndDate,
    decimal Salary,
    string Currency
);