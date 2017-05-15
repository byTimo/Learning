using System;

namespace Garden.Utility
{
    public static class RandomHelper
    {
        private static readonly Random random = new Random();

        public static bool Boolean(double probability = 0.5)
        {
            return random.NextDouble() < probability;
        }

        public static int Int(int max)
        {
            return Between(0, max);
        }

        public static int Between(int first, int second)
        {
            return random.Next(first, second);
        }
    }
}