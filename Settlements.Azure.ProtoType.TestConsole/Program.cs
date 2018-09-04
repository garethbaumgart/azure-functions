using System;
using System.Configuration;
using System.Linq;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Settlements.Azure.ProtoType.TestConsole
{
    public class Program
    {
        private const string StorageConnectionStringKey = "StorageConnectionString";
        private const string SettlementQueueName = "settlement-matching-queue";
        static void Main(string[] args)
        {
            switch (args.FirstOrDefault()?.ToLower())
            {
                case "add":
                    AddMessages(20);
                    break;
                case "read":
                    ReadMessages();
                    break;
                default:
                    AddAndReadMessage();
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("DONE");
            Console.ReadLine();
        }

        public static void AddMessages(int count)
        {
            var storageAccount =
                CloudStorageAccount.Parse(ConfigurationManager.AppSettings.Get(StorageConnectionStringKey));
            var queueClient = storageAccount.CreateCloudQueueClient();
            var messageQueue = queueClient.GetQueueReference(SettlementQueueName);
            for (int index = 1; index <= count; index++)
            {
                messageQueue.AddMessageAsync(new CloudQueueMessage($"{index} - Test Message - {index}")).Wait();
            }
            Console.WriteLine($"Added {count} messages to queue");
        }

        public static void ReadMessages()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings.Get(StorageConnectionStringKey));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue messageQueue = queueClient.GetQueueReference(SettlementQueueName);
            messageQueue.CreateIfNotExistsAsync().Wait();

            while (messageQueue.PeekMessageAsync().Result != null)
            {
                var readMessage = messageQueue.GetMessageAsync();
                Console.WriteLine(readMessage.Result.AsString);
            }
        }

        public static void AddAndReadMessage()
        {
            AddMessages(10);
            ReadMessages();
        }
    }
}
