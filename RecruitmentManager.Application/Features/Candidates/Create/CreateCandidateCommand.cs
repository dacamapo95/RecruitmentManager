using RecruitmentManager.Application.Core.Abstractions;

namespace RecruitmentManager.Application.Features.Candidates.Create;

public sealed record CreateCandidateCommand(
    string FirstName,
    string SurName,
    DateTime DateOfBirth,
    string Email,
    string? PhoneNumber,
    Guid CityId,
    string? ZipCode,
    string? Street,
    int? StateId,
    List<CreateExperienceCommand> Experiences) : ICommand<Guid>;

public sealed record CreateExperienceCommand(
    string Company,
    string Job,
    string Description,
    DateTime StartDate,
    DateTime? EndDate,
    decimal Salary,
    string Currency
);