using Microsoft.Azure.ServiceBus;
using System;
using System.Text;

namespace ServiceBus.Sender
{
    class Program
    {
       static string connectionString = "Endpoint=sb://raytextraining.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=UP3qhVryDpSrpGdf69flyzcmjbXg4Vc2dlXQtgQaHgM=";
       static string qPath = "demoqueue";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var queuClient = new QueueClient(connectionString, qPath);

            for (int i = 0; i < 10; i++)
            {
                var content = $"Message: {i}";
                var message = new Message(Encoding.UTF8.GetBytes(content));
                queuClient.SendAsync(message).Wait();
                
                Console.WriteLine("Sent:"+i);
            }
            Console.WriteLine("Sent messages");
            queuClient.CloseAsync();
            Console.ReadLine();
        }
    }
}
