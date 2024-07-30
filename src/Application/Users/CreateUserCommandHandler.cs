using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;
using Domain.ValueObjects;

namespace Application.Users;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
{
   private readonly IUserRepository _userRepository;
   private readonly IUnitOfWork _unitOfWork;

   public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
   {
      _userRepository = userRepository;
      _unitOfWork = unitOfWork;
   }


   public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
   {
      Result<Email> emailResult = Email.Create(request.Email);
      Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
      Result<LastName> lastNameResult = LastName.Create(request.LastName);
      Result<Username> usernameResult = Username.Create(request.Username);
      Result<Password> passwordResult = Password.Create(request.Password);
      Result<PhoneNumber> phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
      

      if (!await _userRepository.ExistsByEmailAsync(emailResult.Value, cancellationToken))
         return Result.Failure<Guid>(DomainErrors.User.EmailAlreadyInUse);

      Console.WriteLine(passwordResult.Value);

      var user = User.Create(Guid.NewGuid(),firstNameResult.Value, lastNameResult.Value, emailResult.Value, usernameResult.Value, passwordResult.Value, phoneNumberResult.Value, request.IsAdmin, request.IsSuperAdmin, request.IsActive, null);
      user.Addresses = request.Addresses;
      user.Roles = request.Roles;
      await _userRepository.Add(user);

      await _unitOfWork.SaveChangesAsync(cancellationToken);

      return user.Id;

   }
}