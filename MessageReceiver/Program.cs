using Microsoft.Azure.ServiceBus;
using ServiceBusMessagingDemo.Areas.Model;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MessageReceiver
{
    
    
    class Program
    {
        const string connectionstring = "Endpoint=sb://servicebusmessagingdemo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=u90vpjgkh6MHf5zFv9Z6EQuUFSRqXxfS4m2JiCKsBrA=";
        const string queueName = "servicebusmessagingdemo";
        static IQueueClient queueClient;
        static  void Main(string[] args)
        {

            queueClient = new QueueClient(connectionstring, queueName);
            var messageHandelerOption = new MessageHandlerOptions(exceptionReceivedHandler)
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
            var jsonString = Encoding.UTF8.GetString(message.Body);
            User user = new();
            user = JsonSerializer.Deserialize<User>(jsonString);
            Console.WriteLine($"Email: {user.email}   Id: {user.id} Username: {user.Username}");
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private async static Task exceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"ExceptionReceivedEventArgs Closed: { arg.Exception }");
        }
    }
}
