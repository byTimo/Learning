using System;
using System.Linq;
using System.Threading;

namespace TrofiThread
{
    public class EntryPoint
    {
        private static bool IsAlive { get; set; } = true;

        public static void Main(string[] args)
        {
            var first = new Thread(() => PrintPhrase("I'm first thread"));
            var second = new Thread(() => PrintPhrase("I'm second thread"));

            first.Priority = ThreadPriority.Highest;
            second.Priority = ThreadPriority.Highest;
            first.Start();
            second.Start();
            Console.ReadKey();

            GenerateHighload();
            second.Priority = ThreadPriority.Lowest;
            Console.ReadKey();
            IsAlive = false;
            Console.ReadKey();
        }

        private static void GenerateHighload()
        {
            Enumerable.Range(0, 15).Select(x => {
                var t = new Thread(ProcessorHighload);
                t.Priority = ThreadPriority.BelowNormal;
                t.Start();
                return t;
            }).ToArray();

        }

        private static void ProcessorHighload()
        {
            while (IsAlive)
            {
                var i = 4 + 4;
            }
        }

        private static void PrintPhrase(string phrase)
        {
            while (IsAlive)
            {
                Console.WriteLine(phrase);
                Thread.Sleep(500);
            }
        }
    }
}
