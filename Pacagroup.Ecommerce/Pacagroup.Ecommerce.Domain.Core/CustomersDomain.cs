using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;

namespace Pacagroup.Ecommerce.Domain.Core
{
    public class CustomersDomain : ICustomersDomain
    {
        public Task<bool> DeleteAsync(string customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetAsync(string customerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
