using System;

namespace ServiceBus.Management
{
    class Program
    {
        private static string serviceBusConnectionString = "Endpoint=sb://raytextraining1.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=8lB76an/6Trf8XZXWKsdxE+qGjI3PzfAd2DBziX0ttA=";
        static void Main(string[] args)
        {
           // Console.WriteLine("Hello World!");

            ManagementHelper helper = new ManagementHelper(serviceBusConnectionString);

            bool done = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(">");

                string CommandLine = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Magenta;

                string[] Commands = CommandLine.Split(' ');

                try
                {
                    if (Commands.Length>0)
                    {
                        switch (Commands[0])
                        {
                            case "Createqueue":
                            case "cq":
                                if (Commands.Length>1)
                                {
                                    helper.CreateQueueAsync(Commands[1]).Wait();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Queue path not specified");
                                    
                                }
                                break;
                            case "listQueue":
                            case "lq":
                                helper.ListQueuesAsync().Wait();
                                break;
                            case "getQueue":
                            case "gq":
                                if (Commands.Length>1)
                                {
                                    helper.GetQueueAsync(Commands[1]).Wait();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Quepath is not given");
                                }
                                break;
                            case "DeleteQueue":
                            case "dq":
                                if (Commands.Length>1)
                                {
                                    helper.DeleteQueueAsync(Commands[1]).Wait();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Queue path name not given to delete");
                                }
                                break;

                            case "CreateTopic":
                            case "ct":
                                if (Commands.Length>1)
                                {
                                    helper.CreateTopicAsync(Commands[1]).Wait();
                                    
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Topic path is not given");
                                }
                                break;
                            case "CreateSubscription":
                            case "cs":
                                if (Commands.Length>2)
                                {
                                    helper.CreateSubscriptionAsync(Commands[1], Commands[2]).Wait();

                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Topic path and subscription name are not given");
                                }
                                break;
                            case "listTopics":
                            case "lt":
                                helper.ListTopicsAndSubscription().Wait();
                                break;
                            case "exit":
                                done = true;
                                break;
                            default:
                                break;



                        }
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex);
                }
            } while (!done);
        }
    }
}
