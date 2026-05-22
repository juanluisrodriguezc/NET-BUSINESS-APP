using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.Interface.UseCases
{
    public interface IAuthApplication
    {
        Task<Response<bool>> SignUpAsync(SignUpDto signUpDto);
        Task<Response<TokenDto>> SignInAsync(SignInDto signInDto);
    }
}
