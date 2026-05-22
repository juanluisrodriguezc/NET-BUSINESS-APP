using Pacagroup.Ecommerce.Domain.Entities;

namespace Pacagroup.Ecommerce.Transversal.Common
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
