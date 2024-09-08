using Application.Common.Interfaces;
using Application.Users;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
namespace Infrastructure.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }


    public async Task<Guid> RegisterAsync(Email email, Password password, FirstName firstName, LastName lastName, Username username,
        PhoneNumber phoneNumber)
    {
        Console.WriteLine("Registering user");
        var user = new ApplicationUser
        {
            Email = email.Value,
            UserName = username.Value,
            FirstName = firstName.Value,
            LastName = lastName.Value,
            PhoneNumber = phoneNumber.Value
        };
        var passwordHash = new PasswordHasher<ApplicationUser>().HashPassword(user, password.Value);
        Console.WriteLine("User is " + user + passwordHash);
        var createdUser = await this._userManager.CreateAsync(user, passwordHash);
        Console.WriteLine("Created user is " + createdUser);
        if (!createdUser.Succeeded)
        {
            throw new Exception(createdUser.Errors.First().Description);
        }

        return Guid.Parse(user.Id);

    }

    Task IAuthService.LoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    async Task<bool> IAuthService.GetUserAsync(Email email)
    {
        var user = await _userManager.FindByEmailAsync(email.Value);
        return user != null;
    }
}