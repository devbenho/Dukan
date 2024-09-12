using Domain.Shared;
using MediatR;

namespace Application.Common.Interfaces;

public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEvent{}
