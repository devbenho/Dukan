using MediatR;
namespace Domain.Shared;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}
