namespace Infrastructure.Identity.AuthEvents;

public abstract record AuthEventBase(Guid Id) : IAuthEventBase;