using FluentValidation;

namespace Application.Services.User.Commands.Create;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FirstName)
           .NotEmpty().WithMessage("First Name is required")
           .MinimumLength(2).WithMessage("First Name must be at least 2 characters long")
           .MaximumLength(50).WithMessage("First Name cannot exceed 50 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last Name is required")
            .MinimumLength(2).WithMessage("Last Name must be at least 2 characters long")
            .MaximumLength(50).WithMessage("Last Name cannot exceed 50 characters");

        RuleFor(x => x.DateOfBirth)
           .NotEmpty().WithMessage("Date of Birth is required")
           .Must(BeAtLeast18YearsOld).WithMessage("User must be at least 18 years old");

        RuleFor(x => x.PhoneNumber)
           .NotEmpty().WithMessage("Phone Number is required")
           .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone Number must be a valid international phone number");

        RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email is required")
           .EmailAddress().WithMessage("Email must be a valid email address");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches(@"\d").WithMessage("Password must contain at least one number")
            .Matches(@"[\W]").WithMessage("Password must contain at least one special character");
    }

    private bool BeAtLeast18YearsOld(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;

        if (dateOfBirth > today.AddYears(-age))
            age--;

        return age >= 18;
    }
}