using FluentValidation;

namespace Pacagroup.Ecommerce.Application.UseCases.Users.Commands.SignInUser
{
    public class SignInUserValidator : AbstractValidator<SignInUserCommand>
    {
        public SignInUserValidator() {
            RuleFor(u => u.Email).NotNull().NotEmpty();
            RuleFor(u => u.Password).NotNull().NotEmpty().MinimumLength(5);
        }
    }
}
