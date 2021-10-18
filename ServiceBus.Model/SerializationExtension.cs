using Microsoft.Azure.ServiceBus;
using System.Text;
using System.Text.Json;

namespace ServiceBus.Model
{
    public static class SerializationExtension
    {
        public static Message ToMessage<T>(this T input)
        {
            string messageBody = JsonSerializer.Serialize(input);
            return new Message(Encoding.UTF8.GetBytes(messageBody));
        }

        public static T FromMessageTo<T>(this Message message)
        {
            var jsonString = Encoding.UTF8.GetString(message.Body);
            return JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}
