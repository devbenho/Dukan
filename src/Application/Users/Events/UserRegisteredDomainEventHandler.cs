using Application.Common.Interfaces;
using Domain.Events;

namespace Application.Users.Events;

public class UserRegisteredDomainEventHandler : IDomainEventHandler<UserRegisteredDomainEvent> 
{
    
    public Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"User with ID {notification.UserId} has been registered.");
        return Task.CompletedTask;
    }
}