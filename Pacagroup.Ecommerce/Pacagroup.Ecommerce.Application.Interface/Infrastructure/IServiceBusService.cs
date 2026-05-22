namespace Pacagroup.Ecommerce.Application.Interface.Infrastructure
{
    public interface IServiceBusService
    {
        Task SendMessageAsync<T>(string queueName, T message) where T : class;
    }
}
