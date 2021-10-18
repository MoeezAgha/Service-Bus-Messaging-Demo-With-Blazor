using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using ServiceBus.Model;
using System;
using System.Threading.Tasks;

namespace ServiceBusMessagingDemo.Services
{
    public class QueueService : IQueueService
    {
        private readonly IConfiguration configuration;

        public QueueService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendMessageAsync<T>(T serviceBusMessage, string queueName)
        {
            try
            {
                var queueClient = new QueueClient(configuration.GetConnectionString("ConnectionString_AzureServiceBus"), queueName);

                var message = serviceBusMessage.ToMessage();
                await queueClient.SendAsync(message);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
