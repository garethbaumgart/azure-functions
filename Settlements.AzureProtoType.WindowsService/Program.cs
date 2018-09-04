using System;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Settlements.AzurePrototyp.Business;

namespace Settlements.AzureProtoType.WindowsService
{
    public class Program
    {
        private const string StorageConnectionStringKey = "StorageConnectionString";
        private const string SettlementQueueName = "settlement-matching-queue";
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Windows Service");

            ProcessQueueMessages();

            Console.WriteLine("Any key to exit");
            Console.ReadKey();
        }

        public static void ProcessQueueMessages()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings.Get(StorageConnectionStringKey));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue messageQueue = queueClient.GetQueueReference(SettlementQueueName);
            messageQueue.CreateIfNotExistsAsync().Wait();

            while (messageQueue.PeekMessageAsync().Result != null)
            {
                var readMessage = messageQueue.GetMessageAsync();
                var settlementController = new SettlementController();
                var processId = Guid.NewGuid().ToString();
                settlementController.Process(readMessage.Result.AsString, processId);
            }
        }
    }
}
