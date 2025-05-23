using RecruitmentManager.Application.Core.Abstractions;
using RecruitmentManager.Domain.Candidates;
using RecruitmentManager.Domain.Results;

namespace RecruitmentManager.Application.Features.Candidates.Delete;

public class DeleteCandidateCommandHandler(
    ICandidateRepository candidateRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<DeleteCandidateCommand>
{
    private readonly ICandidateRepository _candidateRepository = candidateRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteCandidateCommand command, CancellationToken cancellationToken)
    {
        var candidate = await _candidateRepository.GetByIdAsync(command.Id, cancellationToken);

        if (candidate is null)
        {
            return CandidateErrors.CandidateNotFound(command.Id);
        }

        _candidateRepository.Remove(candidate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}