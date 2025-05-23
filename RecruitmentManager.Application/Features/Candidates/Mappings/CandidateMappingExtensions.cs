using RecruitmentManager.Application.Features.Candidates.Create;
using RecruitmentManager.Application.Features.Candidates.Edit;
using RecruitmentManager.Domain.Candidates;
using RecruitmentManager.Shared;

namespace RecruitmentManager.Application.Features.Candidates.Mappings;

public static class CandidateMappingExtensions
{
    public static CreateCandidateCommand ToCommand(this CreateCandidateRequest request)
    {
        return new CreateCandidateCommand(
            request.FirstName,
            request.SurName,
            request.DateOfBirth,
            request.Email,
            request.PhoneNumber,
            request.CityId,
            request.ZipCode,
            request.Street,
            request.StateId,
            request.Experiences.Select(ToCommand).ToList()
        );
    }

    public static CreateExperienceCommand ToCommand(this CreateExperienceRequest request)
    {
        return new CreateExperienceCommand(
            request.Company,
            request.Job,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.Salary,
            request.Currency
        );
    }

    public static EditCandidateCommand ToCommand(this EditCandidateRequest request, Guid id)
    {
        return new EditCandidateCommand(
            id,
            request.FirstName,
            request.SurName,
            request.DateOfBirth,
            request.Email,
            request.PhoneNumber,
            request.CityId,
            request.ZipCode,
            request.Street,
            request.StateId,
            request.Experiences.Select(ToCommand).ToList()
        );
    }

    private static EditExperienceCommand ToCommand(this EditExperienceRequest request)
    {
        return new EditExperienceCommand(
            request.Id,
            request.Company,
            request.Job,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.Salary,
            request.Currency
        );
    }

    public static CandidateResponse ToResponse(this Candidate candidate)
    {
        return new CandidateResponse(
            candidate.Id,
            candidate.FullName.FirstName,
            candidate.FullName.SurName,
            candidate.DateOfBirth,
            candidate.Email.Value,
            candidate.PhoneNumber,
            candidate.Address?.City.CountryId ?? Guid.Empty,
            candidate.Address?.CityId ?? Guid.Empty,
            candidate.Address?.ZipCode,
            candidate.Address?.Street,
            candidate.StateId,
            candidate.Experiences.Select(ToResponse).ToList()
        );
    }

    public static ExperienceResponse ToResponse(this Experience experience)
    {
        return new ExperienceResponse(
            experience.Id,
            experience.Company,
            experience.Job,
            experience.Description,
            experience.Period.BeginDate,
            experience.Period.EndDate, 
            experience.Salary.Amount,
            experience.Salary.Currency
        );
    }
}
