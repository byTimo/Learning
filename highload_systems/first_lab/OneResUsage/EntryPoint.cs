using System;
using System.Threading;

namespace OneResUsage
{
    class EntryPoint
    {
        private static int resource = 100;
        private static bool IsAlive { get; set; } = true;

        static void Main(string[] args)
        {
            Console.SetCursorPosition(0, 0);
            new Thread(() => ThreadCallback(0, 500)).Start();
            new Thread(() => ThreadCallback(1, 300)).Start();

            Console.ReadKey();
            IsAlive = false;
            Console.ReadKey();
        }

        private static void ThreadCallback(int threadNumber, int delay)
        {
            while (IsAlive)
            {
                if (resource == 0)
                {
                    Console.WriteLine($"Thread {threadNumber} lose!");
                    break;
                }

                if (--resource == 0)
                {
                    Console.WriteLine($"Thread {threadNumber} win!");
                    break;
                }

                Console.SetCursorPosition(0, threadNumber);
                Console.WriteLine($"I have eaten resource. Now it's {resource}");
                Thread.Sleep(delay);
            }
        }
    }
}
