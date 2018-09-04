using System;
using System.Diagnostics;

namespace Settlements.AzurePrototyp.Business
{
    public class SettlementController
    {
        private const string HeaderStart = "<========= New settlement picked up ========>";
        private const string HeaderEnd = "<=================== End ===================>";
        public void Process(string settlement, string processorId)
        {
            PrintDebug(HeaderStart, HeaderEnd, settlement, processorId);
            PrintConsole(HeaderStart, HeaderEnd, settlement, processorId);
        }

        private void PrintDebug(string header, string end, string message, string processorId)
        {
            Debug.WriteLine(header);
            Debug.WriteLine(message);
            Debug.WriteLine(end);
            Debug.WriteLine(string.Empty);
        }

        private void PrintConsole(string header, string end, string message, string processorId)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(header);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"ProcessId => {processorId}");
            Console.WriteLine(message);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(end);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
