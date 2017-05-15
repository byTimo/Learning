using System.Collections.Generic;
using Garden.Flowerbed.Model;

namespace Garden.Utility.Drawing
{
    public interface IDiffCalculator
    {
        IDictionary<Position, PlantSegment> Compare(IEnumerable<IGrowable> first, IEnumerable<IGrowable> second);

        IDictionary<Position, PlantSegment> Compare(IDictionary<Position, PlantSegment> first,
                                                    IEnumerable<IGrowable> second);
    }
}