using System;

namespace Garden
{
    internal class EntryPoint
    {
        public static void Main(string[] args)
        {
            Yard.Seed(100).WatchWhile(() => !Console.KeyAvailable).Wait();
        }
    }
}