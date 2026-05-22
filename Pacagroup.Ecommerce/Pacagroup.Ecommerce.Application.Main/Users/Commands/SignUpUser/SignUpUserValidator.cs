using FluentValidation;

namespace Pacagroup.Ecommerce.Application.UseCases.Users.Commands.SignUpUser
{
    public class SignUpUserValidator : AbstractValidator<SignUpUserCommand>
    {
        public SignUpUserValidator() {
            RuleFor(u => u.FirstName).NotNull().NotEmpty();
            RuleFor(u => u.LastName).NotNull().NotEmpty();
            RuleFor(u => u.UserName).NotNull().NotEmpty();
            RuleFor(u => u.Email).NotNull().NotEmpty();
            RuleFor(u => u.Password).NotNull().NotEmpty().MinimumLength(5);
        }
    }
}
