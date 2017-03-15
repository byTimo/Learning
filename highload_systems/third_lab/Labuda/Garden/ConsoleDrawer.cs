using System;
using System.Collections.Generic;
using Garden.Flowerbed;

namespace Garden
{
    internal class ConsoleDrawer : IDrawer
    {
        private const string Flower = "\u0023";
        private const string Sqrout = "\u2551";
        private const string Seed = "\u2592";

        private const ConsoleColor FlowerColor = ConsoleColor.White;
        private const ConsoleColor SqroutColor = ConsoleColor.Green;
        private const ConsoleColor SeedColor = ConsoleColor.Yellow;

        public ConsoleDrawer()
        {
            Console.CursorVisible = false;
        }

        public void Draw(IDictionary<int, Plant> plants)
        {
            Console.Clear();
            foreach (var plant in plants)
            {
                DrawPlant(plant.Key * 2, plant.Value.Parts);
            }
        }

        private static void DrawPlant(int x, IEnumerable<PlantPart> parts)
        {
            var currentY = 40;
            foreach (var part in parts)
            {
                Console.SetCursorPosition(x, currentY);
                switch (part)
                {
                    case PlantPart.Seed:
                        DrawSeed();
                        break;
                    case PlantPart.Sqrout:
                        DrawSqrout();
                        break;
                    case PlantPart.Flower:
                        DrawFlower();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                currentY--;
            }
        }

        private static void DrawFlower()
        {
            Console.ForegroundColor = FlowerColor;
            Console.Write(Flower);
        }

        private static void DrawSqrout()
        {
            Console.ForegroundColor = SqroutColor;
            Console.Write(Sqrout);
        }

        private static void DrawSeed()
        {
            Console.ForegroundColor = SeedColor;
            Console.Write(Seed);
        }

    }
}