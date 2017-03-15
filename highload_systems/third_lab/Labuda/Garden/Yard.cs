using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Garden.Flowerbed;

namespace Garden
{
    internal class Yard
    {
        private const double FlowerProbability = 0.4;
        private const int DrawingIntervalMilliseconds = 500;

        private static readonly Random random = new Random();
        private readonly IDictionary<int, Plant> plants;
        private readonly IDrawer drawer;
        private readonly ManualResetEvent drawingEvent = new ManualResetEvent(false);

        private Yard(IEnumerable<Plant> plants, IDrawer drawer)
        {
            this.plants = plants.Select((x, i) => Tuple.Create(i, x)).ToDictionary(k => k.Item1, v => v.Item2);
            this.drawer = drawer;
        }

        public static Yard Plant(int flowersCount)
        {
            if (flowersCount < 0)
                throw new ArgumentException("Ты пытаешься посадить что-то не то!");

            return new Yard(Enumerable.Range(0, flowersCount).Select(x => new Plant()), new ConsoleDrawer());
        }

        public Task Watch()
        {
            return Watch(CancellationToken.None);
        }

        public Task Watch(CancellationToken token)
        {
            return Watch(token, () => false);
        }

        public Task WatchBefor(Func<bool> beforCallback)
        {
            return Watch(CancellationToken.None, beforCallback);
        }

        private Task Watch(CancellationToken token, Func<bool> callback)
        {
            return Task.Run(async () =>
            {
                StartGrow(token);
                while (!callback.Invoke())
                {
                    token.ThrowIfCancellationRequested();
                    drawingEvent.Set();
                    await Task.Delay(DrawingIntervalMilliseconds, token);
                    drawer.Draw(plants);
                    drawingEvent.Set();
                }
            }, token);

        }

        private void StartGrow(CancellationToken token)
        {
            plants.Values.Select(x => Task.Run(async () =>
                {
                    while (true)
                    {
                        token.ThrowIfCancellationRequested();
                        await Task.Delay(random.Next(100, 3000), token);
                        drawingEvent.WaitOne();
                        x.Grow(FlowerProbability);
                    }
                }, token))
                .Enumerate();
        }
    }
}