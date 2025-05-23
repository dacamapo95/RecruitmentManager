namespace RecruitmentManager.Shared;

public sealed record CandidateResponse(
    Guid Id,
    string FirstName,
    string SurName,
    DateTime DateOfBirth,
    string Email,
    string? PhoneNumber,
    Guid? CountryId,
    Guid CityId,
    string? ZipCode,
    string? Street,
    int StateId,
    List<ExperienceResponse> Experiences
);