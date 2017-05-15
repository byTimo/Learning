using System;

namespace Simulation.Lib
{
    public struct RandomValue
    {
        public int Dimension { get; }
        public double[] Values { get; }

        public RandomValue(double[] values)
        {
            Values = values;
            Dimension = values.Length;
        }
    }
}
