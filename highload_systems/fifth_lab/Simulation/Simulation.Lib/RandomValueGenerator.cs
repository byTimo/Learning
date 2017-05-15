using System;
using System.Collections.Generic;
using System.Linq;

namespace Simulation.Lib
{
    public class RandomValueGenerator
    {
        private readonly Random random;

        public RandomValueGenerator()
        {
            random = new Random();
        }

        public RandomValueGenerator(int seed)
        {
            random = new Random(seed);
        }

        public RandomValue Generate(int dimension)
        {
            if (dimension < 1)
                return new RandomValue(Array.Empty<double>());
           

            var values = new List<double>(dimension) {random.NextDouble()};

            for (var i = 2; i <= dimension; i++)
            {
                values.Add(Math.Sqrt(1.0 - values.Sum(x => Math.Pow(x, 2.0))));
            }

            return new RandomValue(values.ToArray());
        }
    }
}