using Domain.ValueObjects;

namespace Application.Common.Interfaces;

public interface IAuthService {
    Task<Guid> RegisterAsync(Email email, Password password, FirstName firstName, LastName lastName, Username username, PhoneNumber phoneNumber);
    Task LoginAsync(string email, string password);
    Task GetUserAsync(string email);
}
