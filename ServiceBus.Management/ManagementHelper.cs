using Microsoft.Azure.ServiceBus.Management;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Management
{
    class ManagementHelper
    {
        private ManagementClient m_ManagementClient;

        public ManagementHelper(string connectionString)
        {
            m_ManagementClient = new ManagementClient(connectionString);
        }


        public async Task CreateQueueAsync(string queuePath)
        {
            Console.WriteLine("Creating a queue {0} ...", queuePath);
            var description = GetQueueDescription(queuePath);
            var createdDescription = await m_ManagementClient.CreateQueueAsync(description);
            Console.WriteLine("Done!");
        }

        public QueueDescription GetQueueDescription(string path)
        {
            return new QueueDescription(path)
            {
                RequiresDuplicateDetection = true,
                DuplicateDetectionHistoryTimeWindow = TimeSpan.FromMinutes(10),
                RequiresSession = true,
                MaxDeliveryCount = 20,
                DefaultMessageTimeToLive = TimeSpan.FromHours(1),
                EnableDeadLetteringOnMessageExpiration = true
            };
        }
        public async Task DeleteQueueAsync(string queuePath)
        {
            Console.WriteLine("Deleting a queue {0}..." + queuePath);
            await m_ManagementClient.DeleteQueueAsync(queuePath);
            Console.WriteLine("Done!");
            

        }

        public async Task ListQueuesAsync()
        {
            IEnumerable<QueueDescription> queueDescriptions = await m_ManagementClient.GetQueuesAsync();

            Console.WriteLine("Listing queues");

            foreach (var qDesc in queueDescriptions)
            {
                Console.WriteLine("\t{0}", qDesc.Path);
            }

            Console.WriteLine("Done");
        }

        public async Task GetQueueAsync(string queuePath)
        {
            QueueDescription queueDescription = await m_ManagementClient.GetQueueAsync(queuePath);
            Console.WriteLine($"Queue description for { queuePath }");
            Console.WriteLine($"    Path:                                   { queueDescription.Path }");
            Console.WriteLine($"    MaxSizeInMB:                            { queueDescription.MaxSizeInMB }");
            Console.WriteLine($"    RequiresSession:                        { queueDescription.RequiresSession }");
            Console.WriteLine($"    RequiresDuplicateDetection:             { queueDescription.RequiresDuplicateDetection }");
            Console.WriteLine($"    DuplicateDetectionHistoryTimeWindow:    { queueDescription.DuplicateDetectionHistoryTimeWindow }");
            Console.WriteLine($"    LockDuration:                           { queueDescription.LockDuration }");
            Console.WriteLine($"    DefaultMessageTimeToLive:               { queueDescription.DefaultMessageTimeToLive }");
            Console.WriteLine($"    EnableDeadLetteringOnMessageExpiration: { queueDescription.EnableDeadLetteringOnMessageExpiration }");
            Console.WriteLine($"    EnableBatchedOperations:                {  queueDescription.EnableBatchedOperations }");
            Console.WriteLine($"    MaxSizeInMegabytes:                     { queueDescription.MaxSizeInMB }");
            Console.WriteLine($"    MaxDeliveryCount:                       { queueDescription.MaxDeliveryCount }");
            Console.WriteLine($"    Status:                                 { queueDescription.Status }");


        }

        public async Task CreateTopicAsync(string tPath)
        {
            Console.WriteLine("Creating a topic...");
            var description = await m_ManagementClient.CreateTopicAsync(tPath);
            Console.WriteLine("Done");
        }

        public async Task CreateSubscriptionAsync(string tPath, string subscriptionName)
        {
            Console.WriteLine($"Creating a subscription {subscriptionName} for {tPath}");
            var subscription = await m_ManagementClient.CreateSubscriptionAsync(tPath, subscriptionName);
            Console.WriteLine("Done");
        }

        public async Task ListTopicsAndSubscription()
        {
            IEnumerable<TopicDescription> topicDescriptions = await m_ManagementClient.GetTopicsAsync();
            Console.WriteLine("Listing topics and subscriptions...");
            foreach (TopicDescription topicDescription in topicDescriptions)
            {
                Console.WriteLine("\t{0}", topicDescription.Path);
                IEnumerable<SubscriptionDescription> subscriptionDescriptions = await m_ManagementClient.GetSubscriptionsAsync(topicDescription.Path);
                foreach (SubscriptionDescription subscriptionDescription in subscriptionDescriptions)
                {
                    Console.WriteLine("\t\t{0}", subscriptionDescription.SubscriptionName);
                }
            }
            Console.WriteLine("Done!");
        }

    }
}
