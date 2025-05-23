using NSubstitute;
using RecruitmentManager.Application.Core.Abstractions;
using RecruitmentManager.Application.Features.Candidates.Delete;
using RecruitmentManager.Domain.Candidates;
using RecruitmentManager.Domain.Results;

namespace RecruitmentManager.Tests.Features.Candidates.Delete;

public class DeleteCandidateCommandHandlerTests
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DeleteCandidateCommandHandler _handler;

    public DeleteCandidateCommandHandlerTests()
    {
        _candidateRepository = Substitute.For<ICandidateRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        
        _handler = new DeleteCandidateCommandHandler(
            _candidateRepository,
            _unitOfWork);
    }

    [Fact]
    public async Task Handle_WhenCandidateExists_ShouldRemoveCandidateAndSaveChanges()
    {
        var candidateId = Guid.NewGuid();
        var command = new DeleteCandidateCommand(candidateId);
        var candidate = new Candidate { Id = candidateId };

        _candidateRepository.GetByIdAsync(candidateId, Arg.Any<CancellationToken>())
            .Returns(candidate);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsValid);
        _candidateRepository.Received(1).Remove(candidate);
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenCandidateDoesNotExist_ShouldReturnCandidateNotFoundError()
    {
        var candidateId = Guid.NewGuid();
        var command = new DeleteCandidateCommand(candidateId);

        _candidateRepository.GetByIdAsync(candidateId, Arg.Any<CancellationToken>())
            .Returns((Candidate)null!);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsValid);
        Assert.Equal(result.Error.ErrorType, ErrorTypeEnum.NotFound);   
        _candidateRepository.DidNotReceive().Remove(Arg.Any<Candidate>());
        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}