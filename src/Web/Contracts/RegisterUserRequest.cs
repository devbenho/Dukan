using Domain.ValueObjects;

namespace Web.Contracts;

public sealed record RegisterUserRequest(
    string FirstName,
    string Email,
    string LastName,
    string Username,
    string Password,
    string PhoneNumber,
    bool IsAdmin,
    bool IsSuperAdmin,
    bool IsActive,
    ICollection<Address> Addresses,
    ICollection<UserRole> Roles
);