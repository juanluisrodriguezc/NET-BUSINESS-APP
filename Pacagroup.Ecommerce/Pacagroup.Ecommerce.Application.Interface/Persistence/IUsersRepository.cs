using Pacagroup.Ecommerce.Domain.Entities;

namespace Pacagroup.Ecommerce.Application.Interface.Persistence
{
    public interface IUsersRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> CreateUserAsync(User user, string password);
        Task<bool> CheckPasswordAsync(User user, string password);
    }
}
