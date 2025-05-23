using MediatR;
using RecruitmentManager.Domain.Results;

namespace RecruitmentManager.Application.Core.Abstractions;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;