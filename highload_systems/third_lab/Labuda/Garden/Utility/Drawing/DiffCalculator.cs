using System.Collections.Generic;
using System.Linq;
using Garden.Flowerbed.Model;

namespace Garden.Utility.Drawing
{
    public class DiffCalculator : IDiffCalculator
    {
        private readonly IBlockProjector projector;

        public DiffCalculator() : this(new BlockProjector()) { }

        public DiffCalculator(IBlockProjector projector)
        {
            this.projector = projector;
        }

        public IDictionary<Position, PlantSegment> Compare(IEnumerable<IGrowable> first, IEnumerable<IGrowable> second)
        {
            return Compare(Normalize(first), second);
        }

        public IDictionary<Position, PlantSegment> Compare(IDictionary<Position, PlantSegment> first, IEnumerable<IGrowable> second)
        {
            return Normalize(second).Where(x => !first.ContainsKey(x.Key) || !first[x.Key].Equals(x.Value))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        private Dictionary<Position, PlantSegment> Normalize(IEnumerable<IGrowable> plants)
        {
            return plants.SelectMany((x, i) => projector.Project(x.Nodes, i))
                .GroupBy(x => x.Key, x => x.Value)
                .ToDictionary(x => x.Key, x => x.First());
        }
    }
}