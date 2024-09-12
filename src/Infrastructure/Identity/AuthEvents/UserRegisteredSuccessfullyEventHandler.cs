namespace Infrastructure.Identity.AuthEvents;

public class UserRegisteredSuccessfullyEventHandler : IAuthEventHandler<UserRegisteredSuccessfullyEvent> 
{
    
    public Task Handle(UserRegisteredSuccessfullyEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"User with ID {notification.UserId} has been registered.");
        return Task.CompletedTask;
    }
}