using FluentValidation;
namespace RecruitmentManager.Application.Features.Candidates.Create;
public sealed class CreateCandidateValidator : AbstractValidator<CreateCandidateCommand>
{
    public CreateCandidateValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");

        RuleFor(x => x.SurName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(150).WithMessage("Last name cannot exceed 150 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("A valid email address is required")
            .MaximumLength(250)
            .WithMessage("Email MaximumLength 250");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required")
            .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(15).WithMessage("Phone number cannot exceed 15 characters");

        RuleFor(x => x.CityId)
            .NotEmpty().WithMessage("City is required");

        RuleFor(x => x.Street)
            .MaximumLength(200).WithMessage("Street cannot exceed 200 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.Street));

        RuleFor(x => x.ZipCode)
            .MaximumLength(20).WithMessage("Zip code cannot exceed 20 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.ZipCode));

        RuleForEach(x => x.Experiences).ChildRules(experience =>
        {
            experience.RuleFor(x => x.Company)
                .NotEmpty().WithMessage("Company name is required")
                .MaximumLength(100).WithMessage("Company name cannot exceed 100 characters");

            experience.RuleFor(x => x.Job)
                .NotEmpty().WithMessage("Job title is required")
                .MaximumLength(100).WithMessage("Job title cannot exceed 100 characters");

            experience.RuleFor(x => x.Description)
                .MaximumLength(4000).WithMessage("Description cannot exceed 4000 characters");

            experience.RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required");

            experience.RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("End date must be after or equal to start date");

            experience.RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than 0");

            experience.RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required")
                .MaximumLength(3).WithMessage("Currency cannot exceed 3 characters")
                .MinimumLength(3).WithMessage("Currency minimun length 3 characters");
        });
    }
}
