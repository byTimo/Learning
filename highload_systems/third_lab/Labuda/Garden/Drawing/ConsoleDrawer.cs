using System;
using System.Collections.Generic;
using System.Linq;
using Garden.Flowerbed;

namespace Garden.Drawing
{
    public class ConsoleDrawer : IDrawer
    {
        private const int TopOfConsole = 40;

        private readonly IDiffCalculator diffCalculator;
        private IDictionary<Position, PlantSegment> previousState = new Dictionary<Position, PlantSegment>();

        public ConsoleDrawer(IDiffCalculator diffCalculator)
        {
            this.diffCalculator = diffCalculator;
            Console.CursorVisible = false;
        }

        public void Draw(IEnumerable<IGrowable> plants)
        {
            var plantArray = plants.ToArray();
            DrawDiffs(diffCalculator.Compare(previousState, plantArray));
        }

        private void DrawDiffs(IDictionary<Position, PlantSegment> diffs)
        {
            foreach (var diff in diffs)
            {
                Console.SetCursorPosition(diff.Key.X, TopOfConsole - diff.Key.Y);
                DrawSegment(diff.Value);
                previousState[diff.Key] = diff.Value;
            }
        }

        private void DrawSegment(PlantSegment segment)
        {
            switch (segment)
            {
                case PlantSegment.Seed:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("=");
                    break;
                case PlantSegment.Sqrout:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("|");
                    break;
                case PlantSegment.Flower:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("#");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(segment), segment, null);
            }
        }
    }
}