using Domain.ValueObjects;
using Infrastructure.Identity.AuthEvents;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    
    private List<IAuthEventBase> _events = new();
    
    public IEnumerable<IAuthEventBase> GetAuthEvents()
    {
        Console.WriteLine("Returning events");
        return _events;
    }

    public void ClearAuthEvents()
    {
        foreach (var property in this.GetType().GetProperties())
        {
            if (property.PropertyType == typeof(Email))
            {
                property.SetValue(this, null);
            }
        }
    }
    
    public Task<bool> RaiseAuthEvent(IAuthEventBase authEvent)
    {
        _events.Add(authEvent);
        Console.WriteLine("Event raised");
        return Task.FromResult(true);
    }
}