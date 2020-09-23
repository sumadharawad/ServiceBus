using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBus.Receiver
{
    class Program
    {
        static string connectionString = "Endpoint=sb://raytextraining.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=UP3qhVryDpSrpGdf69flyzcmjbXg4Vc2dlXQtgQaHgM=";
        static string qPath = "demoqueue";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var queuClient = new QueueClient(connectionString, qPath);

            queuClient.RegisterMessageHandler(processMessages, handlerErrors);
            Console.ReadLine();
            
            queuClient.CloseAsync().Wait();
          

        }

        private static  Task handlerErrors(ExceptionReceivedEventArgs arg)
        {
            throw new NotImplementedException();
        }

        private static async Task processMessages(Message message, CancellationToken arg2)
        {
            var content = Encoding.UTF8.GetString(message.Body);
            Console.WriteLine($"Received:{content} ");
        }
    }
}
