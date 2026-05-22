using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pacagroup.Ecommerce.Application.Interface.Infrastructure;
using System.Text.Json;

namespace Pacagroup.Ecommerce.Infrastructure.Services
{
    public class ServiceBusService : IServiceBusService
    {
        private readonly ServiceBusClient _client;
        private readonly ILogger<ServiceBusService> _logger;

        public ServiceBusService(IConfiguration configuration, ILogger<ServiceBusService> logger)
        {
            var connectionString = configuration.GetConnectionString("ServiceBus") ?? configuration["ServiceBus"];
            _client = new ServiceBusClient(connectionString);
            _logger = logger;

        }
        public async Task SendMessageAsync<T>(string queueName, T message) where T : class
        {
            var sender = _client.CreateSender(queueName);
            var messageBody = JsonSerializer.Serialize(message);

            var serviceBusMessage = new ServiceBusMessage(messageBody) {
                ContentType = "application/json",
                Subject = typeof(T).Name,
                MessageId = Guid.NewGuid().ToString()
            };

            await sender.SendMessageAsync(serviceBusMessage);
            _logger.LogInformation("Message sent to queue {queueName}", queueName);

            await sender.DisposeAsync();
        }
    }
}
