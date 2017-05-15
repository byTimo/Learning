using System.Collections.Generic;
using Garden.Genetics;

namespace Garden.Flowerbed.Model
{
    public interface IGrowable
    {
        int GrowingTime { get; }
        bool IsGrowed { get; }
        PlantSegment[] Segments { get; }
        KeyValuePair<GrowingPosition, PlantSegment>[] Nodes { get; }
        IGrowable Grow();
    }
}