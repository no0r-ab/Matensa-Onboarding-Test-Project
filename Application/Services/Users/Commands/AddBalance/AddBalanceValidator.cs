using FluentValidation;
using Application.Services.Users.Commands.AddBalance;

namespace Application.Services.Users.Commands.Validators
{
    public class AddBalanceCommandValidator : AbstractValidator<AddBalanceCommand>
    {
        public AddBalanceCommandValidator()
        {
            RuleFor(command => command.UserId)
                .NotEqual(Guid.Empty).WithMessage("User ID is required.");

            RuleFor(command => command.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
        }
    }
}