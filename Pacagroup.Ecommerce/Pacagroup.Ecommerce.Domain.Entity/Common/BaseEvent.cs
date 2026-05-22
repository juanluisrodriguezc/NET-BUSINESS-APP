namespace Pacagroup.Ecommerce.Domain.Common
{
    public abstract class BaseEvent
    {
        public Guid MessageId { get; set; }
        public DateTime PublishTime { get; set; }

        protected BaseEvent()
        {
            MessageId = Guid.NewGuid();
            PublishTime = DateTime.UtcNow;
        }
    }
}
