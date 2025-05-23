using MediatR;
using RecruitmentManager.Domain.Results;

namespace RecruitmentManager.Application.Core.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;