using MediatR;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.UseCases.Users.Commands.SignInUser
{
    public sealed record SignInUserCommand: IRequest<Response<TokenDto>>
    {        
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
