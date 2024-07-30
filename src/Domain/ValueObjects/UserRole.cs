using System.Text.Json.Serialization;
using Domain.Errors;

namespace Domain.ValueObjects;
public sealed class UserRole : ValueObject
{
    public string Role { get; init; } = null!;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Role;
    }
    
    private UserRole()
    {
    }
    
    [JsonConstructor]
    private UserRole(string role)
    {
        Role = role;
    }
    
    public static UserRole Create(string role)
    {
        return new UserRole(role);
    }
}

