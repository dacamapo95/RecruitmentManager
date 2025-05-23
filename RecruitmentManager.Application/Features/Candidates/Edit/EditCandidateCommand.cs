using RecruitmentManager.Application.Core.Abstractions;

namespace RecruitmentManager.Application.Features.Candidates.Edit;

public sealed record EditCandidateCommand(
    Guid Id,
    string FirstName,
    string SurName,
    DateTime DateOfBirth,
    string Email,
    string? PhoneNumber,
    Guid CityId,
    string? ZipCode,
    string? Street,
    int StateId,
    List<EditExperienceCommand> Experiences) : ICommand;

public sealed record EditExperienceCommand(
    Guid? Id, 
    string Company,
    string Job,
    string Description,
    DateTime StartDate,
    DateTime? EndDate,
    decimal Salary,
    string Currency
);