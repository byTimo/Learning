using System;
using System.Collections.Generic;
using System.Linq;
using Garden.Flowerbed;

namespace Garden.Drawing
{
    public class DiffCalculator : IDiffCalculator
    {
        public IDictionary<Position, PlantSegment> Compare(IEnumerable<IGrowable> first, IEnumerable<IGrowable> second)
        {
            return Compare(Normalize(first), second);
        }

        public IDictionary<Position, PlantSegment> Compare(IDictionary<Position, PlantSegment> first, IEnumerable<IGrowable> second)
        {
            return Normalize(second).Where(x => !first.ContainsKey(x.Key) || first[x.Key] != x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        private static Dictionary<Position, PlantSegment> Normalize(IEnumerable<IGrowable> plants)
        {
            return plants.SelectMany((x, i) => x.Segments.Select((y, j) => Tuple.Create(new Position(i, j), y)))
                .ToDictionary(x => x.Item1, x => x.Item2);
        }
    }
}