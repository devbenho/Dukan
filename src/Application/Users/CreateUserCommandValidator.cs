using FluentValidation;

namespace Application.Users;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(v => v.FirstName)
            .NotEmpty().WithMessage("FirstName is Required.");

        RuleFor(v => v.LastName)
            .NotEmpty().WithMessage("Last name is required.");
        
        RuleFor(v => v.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(200).WithMessage("Email must not exceed 200 characters.")
            .EmailAddress().WithMessage("Email is not a valid email address.");

    }   
    
}