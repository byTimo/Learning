using System;
using Garden.Flowerbed;

namespace Garden
{
    internal class EntryPoint
    {
        public static void Main(string[] args)
        {
            Yard.DigOut(100)
                .Seed(new Shrub())
                .Seed(new Shrub())
                .Seed(new Shrub())
                .Seed(new Shrub())
                .Seed(new Shrub())
                .Seed(new Shrub())
                .Seed(new Shrub())
                .Seed(new Shrub())
                .Seed(new Shrub())
                .Seed(new Shrub())
                .Seed(new Shrub())
                .Seed(new Shrub())
                .Seed(new Shrub())
                .Seed(new Shrub())
                .Seed(new Shrub())
                .Seed(new Shrub())
                .WatchWhile(() => !Console.KeyAvailable)
                .Wait();
        }
    }
}