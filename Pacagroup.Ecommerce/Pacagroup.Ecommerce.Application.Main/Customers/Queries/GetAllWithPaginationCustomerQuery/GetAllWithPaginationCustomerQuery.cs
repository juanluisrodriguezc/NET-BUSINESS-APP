using MediatR;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.UseCases.Customers.Queries.GetAllWithPaginationCustomerQuery
{
    public sealed record GetAllWithPaginationCustomerQuery : IRequest<ResponsePagination<IEnumerable<CustomerDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
