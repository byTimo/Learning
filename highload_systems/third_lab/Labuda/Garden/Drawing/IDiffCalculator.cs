using System.Collections.Generic;
using Garden.Flowerbed;

namespace Garden.Drawing
{
    public interface IDiffCalculator
    {
        IDictionary<Position, PlantSegment> Compare(IEnumerable<IGrowable> first, IEnumerable<IGrowable> second);

        IDictionary<Position, PlantSegment> Compare(IDictionary<Position, PlantSegment> first,
                                                    IEnumerable<IGrowable> second);
    }
}