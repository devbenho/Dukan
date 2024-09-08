using Application.Common.Interfaces;
using Domain.Shared;
using Domain.ValueObjects;

namespace Application.Users;

public record CreateUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string Username,
    string Password,
    string PhoneNumber,
    bool IsAdmin,
    bool IsSuperAdmin,
    bool IsActive,
    ICollection<Address> Addresses,
    ICollection<UserRole> Roles
    ) : ICommand<Guid>
{}
    
