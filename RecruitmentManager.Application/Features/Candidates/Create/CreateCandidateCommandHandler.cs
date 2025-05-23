using RecruitmentManager.Application.Core.Abstractions;
using RecruitmentManager.Domain.Candidates;
using RecruitmentManager.Domain.Results;
using RecruitmentManager.Domain.ValueObjects;

namespace RecruitmentManager.Application.Features.Candidates.Create;

public class CreateCandidateCommandHandler(IUnitOfWork unitOfWork, ICandidateRepository candidateRepository) : ICommandHandler<CreateCandidateCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICandidateRepository _candidateRepository = candidateRepository;

    public async Task<Result<Guid>> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
    {
        if (await _candidateRepository.ExistsAsync(candidate => candidate.Email.Value == request.Email, cancellationToken))
        {
            return CandidateErrors.CandidateAlreadyExists(request.Email);
        }

        Result<FullName> fullNameResult = FullName.Create(request.FirstName, request.SurName);

        if (fullNameResult.IsFailure) return fullNameResult.Error;

        Result<Email> emailResult = Email.Create(request.Email);

        if (emailResult.IsFailure) return emailResult.Error;

        var address = Address.Create(request.Street, request.CityId, request.ZipCode);

        Result<List<Experience>> experiencesResult = CreateExperiences(request.Experiences);

        if (experiencesResult.IsFailure) return experiencesResult.Error;

        var candidate = new Candidate()
        {
            FullName = fullNameResult.Value,
            Email = emailResult.Value,
            DateOfBirth = request.DateOfBirth,
            PhoneNumber = request.PhoneNumber,
            Address = address,
            StateId = request.StateId.HasValue ? request.StateId.Value : (int)StateEnum.New,
            Experiences = experiencesResult.Value,
        };

        _candidateRepository.Add(candidate);

        await _unitOfWork.SaveChangesAsync();

        return candidate.Id;
    }

    private Result<List<Experience>> CreateExperiences(List<CreateExperienceCommand> experiences)
    {
        var experienceList = new List<Experience>();
        foreach (var experience in experiences)
        {
            Result<Period> period = Period.Create(experience.StartDate, experience.EndDate);

            if (period.IsFailure) return period.Error;

            Result<Salary> salary = Salary.Create(experience.Salary, experience.Currency);

            if (salary.IsFailure) return salary.Error;

            var experienceEntity = new Experience()
            {
                Company = experience.Company,
                Job = experience.Job,
                Description = experience.Description,
                Period = period.Value,
                Salary = salary.Value
            };

            experienceList.Add(experienceEntity);
        }

        return experienceList;
    }
}
