namespace Domain.Events;

public sealed record UserRegisteredDomainEvent(Guid Id, Guid UserId) : IDomainEvent;
