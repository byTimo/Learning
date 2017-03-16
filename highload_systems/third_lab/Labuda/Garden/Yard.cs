using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Garden.Drawing;
using Garden.Flowerbed;

namespace Garden
{
    internal class Yard
    {
        private const int DrawingIntervalMilliseconds = 100;

        private static readonly Random random = new Random();
        private readonly IDrawer drawer = new ConsoleDrawer(new DiffCalculator());
        private readonly ManualResetEvent drawingEvent = new ManualResetEvent(true);
        private readonly IGrowable[] plants;

        public Yard(IEnumerable<IGrowable> plants)
        {
            this.plants = plants.ToArray();
        }

        public static Yard Seed(int flowersCount)
        {
            if (flowersCount < 0)
                throw new ArgumentException("Ты пытаешься посадить что-то не то!");

            return new Yard(Enumerable.Range(0, flowersCount).Select(x => SeedAnything()));
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
                        await Task.Delay(random.Next(1000, 3000), token);
                        drawingEvent.WaitOne();
                        x.Grow();
                    }
                }, token))
                .Enumerate();
        }

        private static IGrowable SeedAnything()
        {
            return random.NextDouble() > 0.8d ? (IGrowable) new Flower() : new Gress();
        }
    }
}