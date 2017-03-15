using System;

namespace Garden
{
    internal class EntryPoint
    {
        public static void Main(string[] args)
        {
            Yard.Plant(25).WatchBefor(() => Console.KeyAvailable).Wait();
        }
    }
}