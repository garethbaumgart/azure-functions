using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace Settlements.AzureProtoType.Functions
{
    public static class QueueFunction
    {
        [FunctionName("TestQueueFunction")]
        public static void Run([QueueTrigger("settlement-matching-queue", Connection = "")]string myQueueItem, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {myQueueItem}");
            throw new Exception("Forced Exception here.");
        }

        [FunctionName("TestPoisenQueueFunction")]
        public static void RunPoisen([QueueTrigger("settlement-matching-queue-poison")]string myQueueItem, TraceWriter log)
        {
            log.Info($"Item read by poisen queue - {myQueueItem}");
        }
    }
}
