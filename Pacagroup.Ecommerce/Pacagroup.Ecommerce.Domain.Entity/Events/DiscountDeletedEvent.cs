using Pacagroup.Ecommerce.Domain.Common;

namespace Pacagroup.Ecommerce.Domain.Events
{
    public class DiscountDeletedEvent : BaseEvent
    {
        public int Id { get; set; }
    }
}
