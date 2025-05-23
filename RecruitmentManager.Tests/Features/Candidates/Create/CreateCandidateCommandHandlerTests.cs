using NSubstitute;
using RecruitmentManager.Application.Core.Abstractions;
using RecruitmentManager.Application.Features.Candidates.Create;
using RecruitmentManager.Domain.Candidates;
using RecruitmentManager.Domain.Results;
using System.Linq.Expressions;

namespace RecruitmentManager.Tests.Features.Candidates.Create;

public class CreateCandidateCommandHandlerTests
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateCandidateCommandHandler _handler;

    public CreateCandidateCommandHandlerTests()
    {
        _candidateRepository = Substitute.For<ICandidateRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        
        _handler = new CreateCandidateCommandHandler(
            _unitOfWork,
            _candidateRepository);
    }

    [Fact]
    public async Task Handle_WhenValidData_ShouldCreateCandidateAndReturnId()
    {
        var candidateId = Guid.NewGuid();
        var command = CreateValidCommand();

        _candidateRepository
            .ExistsAsync(Arg.Any<Expression<Func<Candidate, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(false);
        
        _candidateRepository
            .When(x => x.Add(Arg.Any<Candidate>()))
            .Do(x => x.Arg<Candidate>().Id = candidateId);

        var result = await _handler.Handle(command, CancellationToken.None);


        Assert.True(result.IsValid);
        Assert.Equal(candidateId, result.Value);
        _candidateRepository.Received(1).Add(Arg.Any<Candidate>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenEmailAlreadyExists_ShouldReturnCandidateAlreadyExistsError()
    {
        var command = CreateValidCommand();

        _candidateRepository
            .ExistsAsync(Arg.Any<Expression<Func<Candidate, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(true);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsValid);
        Assert.Equal(ErrorTypeEnum.BadRequest, result.Error.ErrorType);
        _candidateRepository.DidNotReceive().Add(Arg.Any<Candidate>());
        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenInvalidEmail_ShouldReturnValidationError()
    {
        var command = CreateValidCommand() with { Email = "invalid-email" };

        _candidateRepository
            .ExistsAsync(Arg.Any<Expression<Func<Candidate, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(false);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsValid);
        Assert.Equal(ErrorTypeEnum.Validation, result.Error.ErrorType);
        _candidateRepository.DidNotReceive().Add(Arg.Any<Candidate>());
        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenInvalidFullName_ShouldReturnValidationError()
    {
        var command = CreateValidCommand() with { FirstName = "", SurName = "" };

        _candidateRepository
            .ExistsAsync(Arg.Any<Expression<Func<Candidate, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(false);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsValid);
        Assert.Equal(ErrorTypeEnum.Validation, result.Error.ErrorType);
        _candidateRepository.DidNotReceive().Add(Arg.Any<Candidate>());
        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenInvalidExperiencePeriod_ShouldReturnValidationError()
    {
        var invalidExperience = new CreateExperienceCommand(
            "23232",
            "asdasda",
            "nada",
            DateTime.UtcNow, 
            DateTime.UtcNow.AddYears(-1),
            1000,
            "USD"
        );

        var command = CreateValidCommand() with { Experiences = new List<CreateExperienceCommand> { invalidExperience } };

        _candidateRepository
            .ExistsAsync(Arg.Any<Expression<Func<Candidate, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(false);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsValid);
        Assert.Equal(ErrorTypeEnum.Validation, result.Error.ErrorType);
        _candidateRepository.DidNotReceive().Add(Arg.Any<Candidate>());
        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenInvalidSalary_ShouldReturnValidationError()
    {
        // Arrange
        var invalidExperience = new CreateExperienceCommand(
            "Comañia",
            "Empleo",
            "Esclavo",
            DateTime.UtcNow.AddYears(-2),
            DateTime.UtcNow.AddYears(-1),
            -100, 
            "USD"
        );

        var command = CreateValidCommand() with { Experiences = new List<CreateExperienceCommand> { invalidExperience } };

        _candidateRepository
            .ExistsAsync(Arg.Any<Expression<Func<Candidate, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(false);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsValid);
        Assert.Equal(ErrorTypeEnum.Validation, result.Error.ErrorType);
        _candidateRepository.DidNotReceive().Add(Arg.Any<Candidate>());
        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    private static CreateCandidateCommand CreateValidCommand()
    {
        var experience = new CreateExperienceCommand(
            "Compañia",
            "Desarrollador Back",
            "Desarrollar software malo",
            DateTime.UtcNow.AddYears(-2),
            DateTime.UtcNow.AddYears(-1),
            5000,
            "COP"
        );

        return new CreateCandidateCommand(
            "Dani",
            "Malo",
            DateTime.UtcNow.AddYears(-30),
            "danielcami782@gmail.com",
            "3186606661",
            Guid.NewGuid(),
            "12345",
            "calle falsa 123",
            new List<CreateExperienceCommand> { experience }
        );
    }
}