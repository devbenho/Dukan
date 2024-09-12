using Application.Common.Interfaces;
using Application.Users;
using Domain.Repositories;
using Domain.ValueObjects;
using Infrastructure.Identity.AuthEvents;
using Microsoft.AspNetCore.Identity;
namespace Infrastructure.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IUnitOfWork _unitOfWork;
    public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _unitOfWork = unitOfWork;
    }
    

    public async Task<Guid> RegisterAsync(Email email, Password password, FirstName firstName, LastName lastName, Username username,
        PhoneNumber phoneNumber)
    {
        
        var user = new ApplicationUser
        {
            Email = email.Value,
            UserName = username.Value,
            FirstName = firstName.Value,
            LastName = lastName.Value,
            PhoneNumber = phoneNumber.Value
        };
        var passwordHash = new PasswordHasher<ApplicationUser>().HashPassword(user, password.Value);
        var createdUser = await this._userManager.CreateAsync(user, passwordHash);
        if (!createdUser.Succeeded)
        {
            throw new Exception(createdUser.Errors.First().Description);
        }

        await user.RaiseAuthEvent(new UserRegisteredSuccessfullyEvent(new Guid(), userId: Guid.Parse(user.Id)));
        await _unitOfWork.SaveChangesAsync();
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