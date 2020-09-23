using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBus.Chat
{
    class Program
    {
        static string connectionString = "Endpoint=sb://raytextraining1.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=8lB76an/6Trf8XZXWKsdxE+qGjI3PzfAd2DBziX0ttA=";
        static string TPath = "demoTopic";
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Name:");
            var userName = Console.ReadLine();

            var manager = new ManagementClient(connectionString);
            if (!manager.TopicExistsAsync(TPath).Result)
            {
                manager.CreateTopicAsync(TPath).Wait();
            }

            var description = new SubscriptionDescription(TPath, userName)
            {
                AutoDeleteOnIdle = TimeSpan.FromMinutes(5)
               
            };

            manager.CreateSubscriptionAsync(description).Wait();


            var topicClient = new TopicClient(connectionString, TPath);
            var subscriptionClient = new SubscriptionClient(connectionString, TPath, userName);


            subscriptionClient.RegisterMessageHandler(ProcessmessageAsyc, handleErrorsAsync);

            var helloMassage = new Message(Encoding.UTF8.GetBytes("Has entered the room"));
            helloMassage.Label = userName;
            topicClient.SendAsync(helloMassage).Wait();

            while (true)
            {
                string text = Console.ReadLine();
                if (text.Equals("exit"))
                {
                    break;
                }

                var chatMessage = new Message(Encoding.UTF8.GetBytes(text));
                chatMessage.Label = userName;
                topicClient.SendAsync(chatMessage).Wait();
            }

            var goodByeMessage = new Message(Encoding.UTF8.GetBytes("Has left the building"));

            goodByeMessage.Label = userName;

            topicClient.SendAsync(goodByeMessage).Wait();

            topicClient.CloseAsync().Wait();

        }

        private static Task handleErrorsAsync(ExceptionReceivedEventArgs arg)
        {
            return Task.CompletedTask;
        }

        private static async Task ProcessmessageAsyc(Message message, CancellationToken token)
        {
            var text = Encoding.UTF8.GetString(message.Body);
            Console.WriteLine($"{message.Label} > {text}");
        }
    }
}
