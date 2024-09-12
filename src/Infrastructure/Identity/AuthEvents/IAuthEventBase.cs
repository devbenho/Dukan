using MediatR;

namespace Infrastructure.Identity.AuthEvents;

public interface IAuthEventBase : INotification
{
    
    public Guid Id { get; init; }
}