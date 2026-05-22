using FluentValidation;

namespace Pacagroup.Ecommerce.Application.UseCases.Customers.Commands.CreateCustomerCommand
{
    public class CreateCustomerValidator: AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().NotNull().MinimumLength(5);
        }
    }
}
