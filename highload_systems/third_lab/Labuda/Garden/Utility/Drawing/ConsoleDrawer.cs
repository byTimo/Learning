using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Garden.Flowerbed.Model;

namespace Garden.Utility.Drawing
{
    public class ConsoleDrawer : IDrawer
    {
        private const int TopOfConsole = 40;
        private const string Wood = "|";
        private const string Sqrout = "|";
        private const string Seed = "#";
        private const string Flower = "#";
        private const string Sheet = "#";
        private const string LeftBranch = "\\";
        private const string RightBranch = "/";

        private readonly IDiffCalculator diffCalculator;
        private readonly IDictionary<Position, PlantSegment> previousState = new Dictionary<Position, PlantSegment>();

        public ConsoleDrawer() : this(new DiffCalculator()) { }

        public ConsoleDrawer(IDiffCalculator diffCalculator)
        {
            this.diffCalculator = diffCalculator;
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.UTF8;
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
                if (diff.Key.X >= 0 && diff.Key.Y >= 0)
                {
                    Console.SetCursorPosition(diff.Key.X, TopOfConsole - diff.Key.Y);
                    DrawSegment(diff.Value);
                }
                previousState[diff.Key] = diff.Value;
            }
        }

        private void DrawSegment(PlantSegment segment)
        {
            switch (segment)
            {
                case PlantSegment.Seed:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(Seed);
                    break;
                case PlantSegment.Sqrout:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(Sqrout);
                    break;
                case PlantSegment.Flower:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(Flower);
                    break;
                case PlantSegment.Wood:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(Wood);
                    break;
                case PlantSegment.Sheet:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(Sheet);
                    break;
                case PlantSegment.LeftBranch:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(LeftBranch);
                    break;
                case PlantSegment.RightBranch:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(RightBranch);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(segment), segment, null);
            }
        }
    }
}