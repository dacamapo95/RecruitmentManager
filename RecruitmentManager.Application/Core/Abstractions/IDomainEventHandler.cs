using MediatR;
using RecruitmentManager.Domain.Primitives;

namespace RecruitmentManager.Application.Core.Abstractions;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
