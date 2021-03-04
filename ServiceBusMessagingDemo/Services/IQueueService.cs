using System.Threading.Tasks;

namespace ServiceBusMessagingDemo.Services
{
    public interface IQueueService
    {
        Task SendMessageAsync<T>(T serviceBusMessage, string queueName);
    }
}