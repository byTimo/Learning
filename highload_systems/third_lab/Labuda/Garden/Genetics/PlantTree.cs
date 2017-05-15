using System;
using System.Collections.Generic;
using System.Linq;
using Garden.Flowerbed.Model;

namespace Garden.Genetics
{
    public class PlantTree
    {
        private readonly IList<PlantNode> segments;

        public PlantTree(): this (new[] {PlantSegment.Seed})
        {
        }

        public PlantTree(IEnumerable<PlantSegment> segments)
        {
            this.segments = segments.Select((x, i) => new PlantNode(x, i)).ToList();
            Top = this.segments.Count;
        }

        public PlantNode[] Nodes => segments.ToArray();
        public int Top { get; private set; }

        public PlantTree Add(PlantSegment segment)
        {
            segments.Add(new PlantNode(segment, Top));
            Top++;
            return this;
        }

        public PlantTree Add(PlantSegment segment, int height, int shift)
        {
            segments.Add(new PlantNode(segment, height, shift));
            Top = Math.Max(Top, height);
            return this;
        }

        public PlantTree SwapLast(PlantSegment segment)
        {
            return Swap(segment, Top - 1);
        }

        public PlantTree SwapLast(PlantSegment segmnet, PlantSegment swapingSegment)
        {
            return Swap(segmnet, segments.IndexOf(segments.Last(x => x.Type == segmnet)));
        }

        public PlantTree Swap(PlantSegment segment, int index)
        {
            segments[index] = new PlantNode(segment, index, segments[index].Shift);
            return this;
        }
    }
}