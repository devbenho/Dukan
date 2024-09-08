using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;
using Domain.ValueObjects;

namespace Application.Users;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
{
   private readonly IAuthService _authService;
   private readonly IUnitOfWork _unitOfWork;

   public CreateUserCommandHandler(IAuthService authService, IUnitOfWork unitOfWork)
   {
      _authService = authService;
      _unitOfWork = unitOfWork;
   }

   public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
   {
      var emailResult = Email.Create(request.Email);
      var firstNameResult = FirstName.Create(request.FirstName);
      var lastNameResult = LastName.Create(request.LastName);
      var usernameResult = Username.Create(request.Username);
      var passwordResult = Password.Create(request.Password);
      var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
      if (emailResult.IsFailure || firstNameResult.IsFailure || lastNameResult.IsFailure || usernameResult.IsFailure || passwordResult.IsFailure || phoneNumberResult.IsFailure)
      {
         var error = emailResult.Error ?? firstNameResult.Error ?? lastNameResult.Error ?? usernameResult.Error ?? passwordResult.Error ?? phoneNumberResult.Error;
         return Result.Failure<Guid>(error);
      }


      var userId = await _authService.RegisterAsync(
         emailResult.Value,
         passwordResult.Value,
         firstNameResult.Value,
         lastNameResult.Value,
         usernameResult.Value,
         phoneNumberResult.Value);
      
      return Result.Success(userId);
   }
}