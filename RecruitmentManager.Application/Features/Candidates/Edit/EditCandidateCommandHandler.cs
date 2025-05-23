using RecruitmentManager.Application.Core.Abstractions;
using RecruitmentManager.Domain.Candidates;
using RecruitmentManager.Domain.Results;
using RecruitmentManager.Domain.ValueObjects;

namespace RecruitmentManager.Application.Features.Candidates.Edit;

public class EditCandidateCommandHandler
    (IUnitOfWork unitOfWork,
    ICandidateRepository candidateRepository) : ICommandHandler<EditCandidateCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICandidateRepository _candidateRepository = candidateRepository;

    public async Task<Result> Handle(EditCandidateCommand command, CancellationToken cancellationToken)
    {
        var candidate = await _candidateRepository.GetWithExperiences(command.Id, cancellationToken);

        if (candidate is null)
        {
            return CandidateErrors.CandidateNotFound(command.Id);
        }

        Result<FullName> fullNameResult = FullName.Create(command.FirstName, command.SurName);
        if (fullNameResult.IsFailure) return fullNameResult.Error;

        Result<Email> emailResult = Email.Create(command.Email);

        if (emailResult.IsFailure) return emailResult.Error;

        var address = Address.Create(command.Street, command.CityId, command.ZipCode);

        var experiencesResult = UpdateExperiences(command.Experiences, candidate.Experiences);

        if (experiencesResult.IsFailure) return experiencesResult.Error;

        UpdateCandidateDetails(candidate, fullNameResult.Value, emailResult.Value, command, address);

        _candidateRepository.Update(candidate);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

  
    private static void UpdateCandidateDetails(
        Candidate candidate,
        FullName fullName,
        Email email,
        EditCandidateCommand command,
        Address address)
    {
        candidate.FullName = fullName;
        candidate.Email = email;
        candidate.DateOfBirth = command.DateOfBirth;
        candidate.PhoneNumber = command.PhoneNumber;
        candidate.Address = address;
        candidate.StateId = command.StateId;
    }

    private Result UpdateExperiences(List<EditExperienceCommand> experiencesCommand, ICollection<Experience> experiences)
    {
        var commandIds = experiencesCommand.Where(e => e.Id.HasValue).Select(e => e.Id.Value).ToHashSet();
        var toRemove = experiences.Where(e => !commandIds.Contains(e.Id)).ToList();
        foreach (var exp in toRemove) experiences.Remove(exp);

        foreach (var experience in experiencesCommand)
            if (experience.Id.HasValue)
            {
                var existingExperience = experiences.FirstOrDefault(e => e.Id == experience.Id.Value);
                if (existingExperience == null)
                    return CandidateErrors.ExperienceNotFound(experience.Id.Value);

                var updateResult = UpdateExistingExperience(existingExperience, experience);
                if (updateResult.IsFailure) return updateResult.Error;
            }
            else
            {
                var addResult = CreateAndAddExperience(experience, experiences);
                if (addResult.IsFailure) return addResult.Error;
            }

        return Result.Success();
    }

    private static Result UpdateExistingExperience(Experience existingExperience, EditExperienceCommand experience)
    {
        existingExperience.Company = experience.Company;
        existingExperience.Job = experience.Job;
        existingExperience.Description = experience.Description;

        var periodResult = Period.Create(experience.StartDate, experience.EndDate);
        if (periodResult.IsFailure) return periodResult.Error;

        var salaryResult = Salary.Create(experience.Salary, experience.Currency);
        if (salaryResult.IsFailure) return salaryResult.Error;

        existingExperience.Period = periodResult.Value;
        existingExperience.Salary = salaryResult.Value;

        return Result.Success();
    }

    private static Result CreateAndAddExperience(EditExperienceCommand experience, ICollection<Experience> experiences)
    {
        var period = Period.Create(experience.StartDate, experience.EndDate);
        if (period.IsFailure) return period.Error;

        var salary = Salary.Create(experience.Salary, experience.Currency);
        if (salary.IsFailure) return salary.Error;

        var newExperienceEntity = new Experience
        {
            Company = experience.Company,
            Job = experience.Job,
            Description = experience.Description,
            Period = period.Value,
            Salary = salary.Value
        };

        experiences.Add(newExperienceEntity);
        return Result.Success();
    }
}