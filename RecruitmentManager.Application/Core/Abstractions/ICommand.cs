using MediatR;
using RecruitmentManager.Domain.Results;

namespace RecruitmentManager.Application.Core.Abstractions;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;
