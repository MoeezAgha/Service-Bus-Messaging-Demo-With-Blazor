using Microsoft.Azure.ServiceBus;
using ServiceBus.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MessageReceiver
{
    class Program
    {
        const string connectionstring = "Endpoint=sb://servicebusmessagingdemo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=u90vpjgkh6MHf5zFv9Z6EQuUFSRqXxfS4m2JiCKsBrA=";
        static IQueueClient queueClient;
        static void Main(string[] args)
        {

            queueClient = new QueueClient(connectionstring, Common.QueueName);
            var messageHandelerOption = new MessageHandlerOptions(async arg => Console.WriteLine($"ExceptionReceivedEventArgs Closed: { arg.Exception }"))
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandelerOption);
            Console.ReadLine();
            Console.WriteLine("Connection Closed");
            queueClient.CloseAsync();
        }

        private async static Task ProcessMessagesAsync(Message message, CancellationToken arg2)
        {
            var user = message.FromMessageTo<User>();
            Console.WriteLine($"First name: {user.FirstName}\tLast name: {user.LastName}");
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }
    }
}
