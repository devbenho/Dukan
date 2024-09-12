namespace Infrastructure.Identity.AuthEvents;

public class UserRegisteredSuccessfullyEvent(Guid id, Guid userId): IAuthEventBase
{
    public Guid UserId { get; } = userId;
    public Guid Id { get; init; }
}