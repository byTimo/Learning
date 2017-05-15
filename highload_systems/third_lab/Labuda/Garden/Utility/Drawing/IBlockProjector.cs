using System.Collections.Generic;
using Garden.Flowerbed.Model;

namespace Garden.Utility.Drawing
{
    public interface IBlockProjector
    {
        IEnumerable<KeyValuePair<Position, PlantSegment>> Project(IEnumerable<KeyValuePair<GrowingPosition, PlantSegment>> nodes, int bedPosition);
    }
}