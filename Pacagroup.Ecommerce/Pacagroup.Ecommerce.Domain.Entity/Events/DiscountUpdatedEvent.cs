using Pacagroup.Ecommerce.Domain.Common;
using Pacagroup.Ecommerce.Domain.Enums;

namespace Pacagroup.Ecommerce.Domain.Events
{
    public class DiscountUpdatedEvent : BaseEvent
    {
        public int Id { get; set; }        
        public decimal Percent { get; set; }
        public DiscountStatus Status { get; set; }
    }
}
