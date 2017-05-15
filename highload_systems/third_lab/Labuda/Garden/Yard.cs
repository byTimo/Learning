using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Garden.Flowerbed;
using Garden.Flowerbed.Model;
using Garden.Utility;
using Garden.Utility.Drawing;

namespace Garden
{
    internal class Yard
    {
        private const int DrawingIntervalMilliseconds = 50;
        private readonly IDrawer drawer = new ConsoleDrawer();
        private readonly ManualResetEvent drawingEvent = new ManualResetEvent(true);
        private readonly IGrowable[] plants;

        public Yard(IEnumerable<IGrowable> plants)
        {
            this.plants = plants.ToArray();
        }

        public static Yard DigOut(int gardenBedCount)
        {
            if (gardenBedCount < 0)
                throw new ArgumentException("Ты пытаешься посадить что-то не то!");

            return new Yard(Enumerable.Range(0, gardenBedCount).Select(x => SeedAnything()));
        }

        public Yard Seed(IGrowable plant, int bed = -1)
        {
            bed = bed == -1 ? RandomHelper.Int(plants.Length) : bed;
            plants[bed] = plant;
            return this;
        }

        public Task Watch()
        {
            return Watch(CancellationToken.None);
        }

        public Task Watch(CancellationToken token)
        {
            return Watch(token, () => false);
        }

        public Task WatchWhile(Func<bool> beforCallback)
        {
            return Watch(CancellationToken.None, beforCallback);
        }

        private Task Watch(CancellationToken token, Func<bool> callback)
        {
            return Task.Run(async () =>
            {
                StartGrow(token);
                while (callback.Invoke())
                {
                    token.ThrowIfCancellationRequested();
                    await Task.Delay(DrawingIntervalMilliseconds, token).ConfigureAwait(true);
                    drawingEvent.Set();
                    drawer.Draw(plants);
                    drawingEvent.Set();
                }
            }, token);

        }

        private void StartGrow(CancellationToken token)
        {
            plants.Select(x => Task.Run(async () =>
                {
                    while (true)
                    {
                        token.ThrowIfCancellationRequested();
                        await Task.Delay(x.GrowingTime, token);
                        drawingEvent.WaitOne();
                        x.Grow();
                    }
                }, token))
                .Enumerate();
        }

        private static IGrowable SeedAnything()
        {
            return RandomHelper.Boolean(0.2) ? (IGrowable) new Flower() : new Gress();
        }
    }
}