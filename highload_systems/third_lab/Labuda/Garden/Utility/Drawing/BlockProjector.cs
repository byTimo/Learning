using System.Collections.Generic;
using System.Linq;
using Garden.Flowerbed.Model;
using Garden.Genetics;

namespace Garden.Utility.Drawing
{
    public class BlockProjector : IBlockProjector
    {
        public IEnumerable<KeyValuePair<Position, PlantSegment>> Project(IEnumerable<KeyValuePair<GrowingPosition, PlantSegment>> nodes, int bedPosition)
        {
            return nodes.Select(b => ToKeyValue(b.Value, bedPosition + b.Key.Shift, b.Key.Height));
        }

        private static KeyValuePair<Position, PlantSegment> ToKeyValue(PlantSegment segment, int x, int y)
        {
            return new KeyValuePair<Position, PlantSegment>(new Position(x, y), segment);
        }
    }
}