using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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

        public async Task SendMessageAsync<T>(T serviceBusMessage, string queueName)//servicebusmessagingdemo
        {
            try
            {
                var queueClient = new QueueClient(configuration.GetConnectionString("ConnectionString_AzureServiceBus"), queueName);
                string messageBody = JsonSerializer.Serialize(serviceBusMessage);
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                await queueClient.SendAsync(message);
            }
            catch (Exception e)
            {

                throw;
            }
         

        }
    }
}
