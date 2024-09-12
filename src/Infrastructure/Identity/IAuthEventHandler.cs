using Infrastructure.Identity.AuthEvents;
using MediatR;

namespace Infrastructure.Identity;

public interface IAuthEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IAuthEventBase{}