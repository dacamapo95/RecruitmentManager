using RecruitmentManager.Application.Core.Abstractions;

namespace RecruitmentManager.Application.Features.Candidates.Delete;

/// <summary>
/// Command to delete a candidate by Id.
/// </summary>
public sealed record DeleteCandidateCommand(Guid Id) : ICommand;